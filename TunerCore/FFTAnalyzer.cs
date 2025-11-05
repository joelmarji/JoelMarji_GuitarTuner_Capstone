using System;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

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
        private static string[] NoteNames =
            { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        public FFTAnalyzer(int sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        //Analyzes audio samples and returns closest musical note
        public TuningResult GetNote(float[] samples)
        {
            double freq = GetDominantFreq(samples);

            if (freq <= 0)
            {
                return new TuningResult();
            }

            //Convert frequency to MIDI note number (A4 = 69)
            double noteNumber = 12 * Math.Log((freq / 440), 2) + 69;

            //find closest int note
            int roundedNoteNumber = (int)Math.Round(noteNumber);

            //find note name
            int noteIndex = roundedNoteNumber % 12;
            string name = NoteNames[noteIndex];

            //find octave
            int octave = (roundedNoteNumber / 12) - 1;
            string noteName = name + octave; //e.g. "A4"

            //find deviation in cents (1/100th of a note)
            int cents = (int)((noteNumber - roundedNoteNumber) * 100);

            return new TuningResult
            {
                Frequency = freq,
                NoteName = noteName,
                CentsDeviation = cents
            };
        }

        //Use FFT analysis to find dominant frequency of mic input buffer
        //Private, GetNote is new public API
        private double GetDominantFreq(float[] samples)
        {
            if (samples == null || samples.Length == 0)
            {
                return 0.0;
            }

            // Apply Hann Windowing function to reduce spectral leakage by processing samples
            float[] windowedSamples = new float[samples.Length];
            for (int i=0; i<samples.Length; i++)
            {
                //calculate hann window multiplier
                double multiplier = 0.5 * (1 - Math.Cos(2 * Math.PI * i / (samples.Length - 1)));

                windowedSamples[i] = (float)(samples[i] * multiplier);
            }
            // Load FFT buffer with samples and perform FFT on buffer
            Complex[] fftBuffer = new Complex[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                fftBuffer[i] = new Complex(windowedSamples[i], 0);
            }

            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

            double[] magnitudes = new double[samples.Length / 2];
            double[] hpsMagnitudes = new double[samples.Length / 2]; //Harmonic Product Spectrum 
            for (int i = 0; i < magnitudes.Length; i++)
            {
                magnitudes[i] = fftBuffer[i].Magnitude;
                hpsMagnitudes[i] = fftBuffer[i].Magnitude;
            }

            // Magnitudes logic only finds the most dominant frequency, which may return false 
            // values due to harmonics when being played on an actual instrument. Use Harmonic Product
            // Spectrum algorithm to find the fundamental frequency:

            for (int i = 1; i<magnitudes.Length/4; i++) //4 harmonics to check
            {
                //"Squish" samples 
                hpsMagnitudes[i] *= magnitudes[i * 2];
                hpsMagnitudes[i] *= magnitudes[i * 3];
                hpsMagnitudes[i] *= magnitudes[i * 4];
            }

            // Find peak magnitudes
            int peakIndex = 0;
            double peakValue = 0.0;
            int hpsPeakIndex = 0;
            double hpsPeakValue = 0.0;  
            for (int i = 1; i < magnitudes.Length; i++)
            {
                //find original peak for Quadratic interpolation
                if (magnitudes[i] > peakValue)
                {
                    peakValue = magnitudes[i];
                    peakIndex = i;
                }

                //find peak of hpsMagnitudes
                if (hpsMagnitudes[i] > hpsPeakValue)
                {
                    hpsPeakValue = hpsMagnitudes[i];
                    hpsPeakIndex = i;
                }
            }

            // Threshold to ignore background noise
            //TODO: tune this threshold
            //if (peakValue < 0.1)
            //{
            //    return 0.0;
            //}

            

            // Bounds Check:
            if (peakIndex <= 0 || peakIndex >= magnitudes.Length - 1)
            {
                // Cant interpolate, return basic bin freq
                return ((double)sampleRate / samples.Length) * peakIndex;
            }

            // FFT algorithm will sort frequencies into "bins", use Quadratic interpolation 
            // to find peaks based on peakMagnitude, peakValue, leftMagnitude and rightMagnitude
            double leftMag = magnitudes[peakIndex - 1];
            double rightMag = magnitudes[peakIndex + 1];
            double peakMag = magnitudes[peakIndex];

            // Avoid division by 0
            if (Math.Abs(leftMag - (2 * peakMag) + rightMag) < 0.00001)
            {
                return ((double)sampleRate / samples.Length) * peakIndex;
            }

            double offset = (0.5 * (leftMag - rightMag)) / (leftMag - (2 * peakMag) + rightMag);
            double preciseIndex = peakIndex + offset;


            // Freq = (K/N) * R 
            double dominantFreq = ((double)sampleRate / samples.Length) * preciseIndex;
            return dominantFreq;
        }
    }
}
