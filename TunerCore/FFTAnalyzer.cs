/* File name: GuitarTuner_Console_App_Main.cs
    Description:
    A console application that uses the TunerCore library to capture audio from a microphone,
    analyze the frequency, and display the detected musical note along with its deviation in cents.

    Developed by: Joel Marji
    Date Created: 09/28/2025
    Date Modified: 12/03/2025
*/
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace TunerCore
{
    public struct TuningResult
    {
        public double Frequency;      // 441.2
        public string NoteName;       // "A4"
        public int CentsDeviation;    // 5 (meaning 5 cents sharp)
        public bool isValid;       
    }
    public class FFTAnalyzer
    {
        private int sampleRate;
        public double BaseFrequency { get; set; } = 440.0;
        private List<double> freqHistory = new List<double>();
        private const int HistorySize = 7; // How many samples to average
        private const double MaxJumpHz = 10.0; // If freq jumps more than this, treat as new note

        private static string[] NoteNames =
            { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public FFTAnalyzer(int sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        // Analyzes audio samples and returns closest musical note
        public TuningResult GetNote(float[] samples)
        {
            double instantFreq = GetDominantFreq(samples);

            if (instantFreq <= 0 || instantFreq < 20.0)
            {
                return new TuningResult()
                {
                    isValid = false
                };
            }

            // Stabilize frequency using moving average
            if (freqHistory.Count > 0)
            {
                double currentAvg = freqHistory.Average();
                if (Math.Abs(instantFreq - currentAvg) > MaxJumpHz)
                {
                    freqHistory.Clear();
                }
            }

            freqHistory.Add(instantFreq);
            if (freqHistory.Count > HistorySize)
            {
                freqHistory.RemoveAt(0);
            }

            // Use smoothed frequency for note calculation
            double smoothedFreq = freqHistory.Average();

            // Convert frequency to MIDI note number (A4 = 69)
            // Default tuning A4 = 440Hz
            double noteNumber = 12 * Math.Log((smoothedFreq / BaseFrequency), 2) + 69;

            // Find closest int note
            int roundedNoteNumber = (int)Math.Round(noteNumber);

            // Find note name
            int noteIndex = roundedNoteNumber % 12;
            string name = NoteNames[noteIndex];

            // Find octave
            int octave = (roundedNoteNumber / 12) - 1;
            string noteName = name + octave; //e.g. "A4"

            // Find deviation in cents (1/100th of a note)
            int cents = (int)((noteNumber - roundedNoteNumber) * 100);

            return new TuningResult
            {
                Frequency = smoothedFreq,
                NoteName = noteName,
                CentsDeviation = cents,
                isValid = true
            };
        }

        // Use FFT, Quadratic Interpolation, HPS, and Hann Window to find dominant frequency of mic input buffer
        // Private, GetNote is public API
        private double GetDominantFreq(float[] samples)
        {
            if (samples == null || samples.Length == 0)
            {
                return 0.0;
            }

            // Use RMS Normalization to improve quality of signal
            double avgAmplitude = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                avgAmplitude += samples[i];
            }

            avgAmplitude /= samples.Length;

            for (int i= 0; i < samples.Length; i++)
            {
                samples[i] -= (float)avgAmplitude;
            }

            double sumSquares = 0;
            for (int i = 0; i < samples.Length; i++)
            {
                sumSquares += samples[i] * samples[i];
            }

            double rms = Math.Sqrt(sumSquares / samples.Length);

            if (rms > 0.001)
            {
                double targetRMS = .1; // Target RMS level
                double gain = targetRMS / rms;

                for (int i = 0; i < samples.Length; i++)
                {
                    samples[i] *= (float)gain;
                }
            }

            // Apply Hann Windowing function to reduce spectral leakage by processing samples
            // "Smoothly" tapering the beginning and end of the sample buffer to zero
            float[] windowedSamples = new float[samples.Length];
            for (int i=0; i<samples.Length; i++)
            {
                // Calculate hann window multiplier
                double multiplier = 0.5 * (1 - Math.Cos(2 * Math.PI * i / (samples.Length - 1)));

                windowedSamples[i] = (float)(samples[i] * multiplier);
            }

            // Load FFT buffer with samples
            Complex[] fftBuffer = new Complex[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                fftBuffer[i] = new Complex(windowedSamples[i], 0);
            }

            // Perform FFT
            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

            double[] magnitudes = new double[samples.Length / 2];
            double[] hpsMagnitudes = new double[samples.Length / 2]; //Harmonic Product Spectrum 
            for (int i = 0; i < magnitudes.Length; i++)
            {
                magnitudes[i] = fftBuffer[i].Magnitude;
                hpsMagnitudes[i] = fftBuffer[i].Magnitude;
            }

            // Use Harmonic Product Spectrum (HPS) to reduce octave errors
            // due to harmonics having higher amplitudes than fundamental freq

            for (int i = 1; i<magnitudes.Length/4; i++) // Check 4 harmonics
            {
                //"Squish" samples 
                // Multiply fundamental bin by its harmonics to compensate for internal 
                // microphone rolloff of lower frequencies
                hpsMagnitudes[i] = magnitudes[i] 
                                 + (magnitudes[i * 2] * 0.9)
                                 + (magnitudes[i * 3] * 0.7)
                                 + (magnitudes[i * 4] * 0.5);
            }

            // Find peak magnitudes
            int peakIndex = 0;
            double peakValue = 0.0;
            int hpsPeakIndex = 0;
            double hpsPeakValue = 0.0;

            for (int i = 1; i < magnitudes.Length; i++)
            {
                // Sort original peak for Quadratic interpolation
                if (magnitudes[i] > peakValue)
                {
                    peakValue = magnitudes[i];
                    peakIndex = i;
                }

                //Sort HPS peak for frequency selection
                if (hpsMagnitudes[i] > hpsPeakValue)
                {
                    hpsPeakValue = hpsMagnitudes[i];
                    hpsPeakIndex = i;
                }
            }

            // Threshold to ignore background noise
            if (peakValue < 0.2)
            {
                return 0.0;
            }

            // If HPS signal is too weak, use basic peakIndex
            int indexToUse = hpsPeakIndex;
            /*if (hpsPeakIndex > 0 && hpsPeakValue > (peakValue * 0.01)) 
            {
                indexToUse = hpsPeakIndex;
            }
            else
            {
                indexToUse = peakIndex;
            }*/

            // Bounds Check:
            if (indexToUse <= 0 || indexToUse >= magnitudes.Length - 1)
            {
                // Cant interpolate, return basic bin freq
                return ((double)sampleRate / samples.Length) * indexToUse;
            }

            // FFT algorithm will sort frequencies into "bins", use Quadratic interpolation 
            // to find peaks based on peakMagnitude, peakValue, leftMagnitude and rightMagnitude
            double leftMag = magnitudes[indexToUse - 1];
            double rightMag = magnitudes[indexToUse + 1];
            double peakMag = magnitudes[indexToUse];

            // Avoid division by 0
            if (Math.Abs(leftMag - (2 * peakMag) + rightMag) <= 0)
            {
                return ((double)sampleRate / samples.Length) * indexToUse;
            }

            // Perform Quadratic Interpolation
            double offset = (0.5 * (leftMag - rightMag)) / (leftMag - (2 * peakMag) + rightMag);
            double preciseIndex = indexToUse + offset;

            // Find dominant frequency using precise index
            // Freq = (K/N) * R 
            double dominantFreq = ((double)sampleRate / samples.Length) * preciseIndex;
            return dominantFreq;
        }
    }
}
