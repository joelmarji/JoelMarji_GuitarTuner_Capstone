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

            // Check if audio subsystem is initialized
            if (mics.Length == 0)
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

            // Open device
            if (!audio.OpenDevice(micName))
            {
                Console.WriteLine("Failed to open selected microphone.");
                return;
            }

            // Create FFT analyzer
            FFTAnalyzer analyzer = new FFTAnalyzer(audio.AudioSpec.freq);
            Console.WriteLine($"Mic open. Sample Rate: {audio.AudioSpec.freq} Hz.");
            Console.WriteLine("Recording... press any key to stop.");

            while (!Console.KeyAvailable)
            {
                float[] samples = audio.CaptureSamples(4096);
                if (samples != null && samples.Length > 0)
                {
                    TuningResult result = analyzer.GetNote(samples);
                    string sign;
                    if (result.CentsDeviation >= 0)
                    {
                        // If the number is positive (sharp), add a "+"
                        sign = "+";
                    }
                    else
                    {
                        sign = "";
                    }

                    string output = $"Note: {result.NoteName} | Cents: {sign}{result.CentsDeviation} | Freq: {result.Frequency:F} Hz";
                    string finalOutput = output.PadRight(Console.BufferWidth - 1);
                    Console.Write($"\r{finalOutput}");
                }
                // Avoid tight cpu cycle
                System.Threading.Thread.Sleep(50);
            }
            Console.ReadKey(true);
            Console.WriteLine("END PROGRAM");
        }
    }
}