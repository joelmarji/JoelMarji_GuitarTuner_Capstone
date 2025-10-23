using System;
using System.Net;
using SDL2;
using TunerCore;

class GuitarTuner_Console_App_Main
{
    static void Main()
    {
        using (AudioProcessor audio = new AudioProcessor())
        {
            string[] mics = audio.GetMicrophoneList();

            if (mics.Length == 0)// Check if audio subsystem is initialized
            {
                Console.WriteLine("No audio capture devices detected!");
                return;
            }

            Console.WriteLine("Available Microphones");
            for (int i = 0; i < mics.Length; i++)
            {
                Console.WriteLine($"{i}: {mics[i]}");
            }

            Console.WriteLine("Select microphone: ");
            int micIndex = int.Parse(Console.ReadLine() ?? "0");
            string micName = mics[micIndex];

            // open device
            if (!audio.OpenDevice(micName))
            {
                Console.WriteLine("Failed to open selected microphone.");
                return;
            }

            // create FFT analyzer
            FFTAnalyzer analyzer = new FFTAnalyzer(audio.AudioSpec.freq);
            Console.WriteLine($"Mic open. Sample Rate: {audio.AudioSpec.freq} Hz.");

            Console.WriteLine("Recording... press any key to stop.");

            while (!Console.KeyAvailable)
            {
                float[] samples = audio.CaptureSamples(4096);
                if (samples != null && samples.Length > 0)
                {
                    double freq = analyzer.GetDominantFreq(samples);
                    if (freq > 0)
                    {
                        Console.WriteLine($"Dominant Frequency: {freq:F1} Hz");
                    }
                }
                //avoid tight cpu cycle
                System.Threading.Thread.Sleep(50);
            }
            Console.ReadKey(true);
            Console.WriteLine("END PROGRAM");
        }
    }
}