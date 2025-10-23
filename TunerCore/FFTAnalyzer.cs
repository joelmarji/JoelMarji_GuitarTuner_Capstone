using System;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace TunerCore
{
    public class FFTAnalyzer
    {
        private int sampleRate;

        public FFTAnalyzer(int sampleRate)
        {
            this.sampleRate = sampleRate;
        }

        public double GetDominantFreq(float[] samples)
        {
            if (samples == null || samples.Length == 0)
            {
                return 0.0;
            }

            Complex[] fftBuffer = new Complex[samples.Length];
            for (int i = 0; i < samples.Length; i++) 
            {
                fftBuffer[i] = new Complex(samples[i], 0);
            }

            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

            double[] magnitudes = new double[samples.Length / 2];
            for (int i = 0; i < magnitudes.Length; i++)
            {
                magnitudes[i] = fftBuffer[i].Magnitude;
            }

            int peakIndex = 0;
            double peakValue = 0.0;
            for (int i = 0; i < magnitudes.Length; i++)
            {
                if (magnitudes[i] > peakValue)
                {
                    peakValue = magnitudes[i];
                    peakIndex = i;
                }
            }

            // Freq = (K/N) * R 
            double dominantFreq = ((double)sampleRate / samples.Length) * peakIndex;
            return dominantFreq;
        }


    }
}
