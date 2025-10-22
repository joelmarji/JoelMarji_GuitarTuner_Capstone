using System;
using SDL2;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;

class Program
{
    static void Main()
    {
        if (SDL.SDL_InitSubSystem(SDL.SDL_INIT_AUDIO) < 0)// Check if audio subsystem is initialized
        {
            Console.WriteLine($"SDL_Init Error: {SDL.SDL_GetError()}");
            return;
        }

        string micName = MicSelect();
        if (micName == "")
        {
            Console.WriteLine("END PROGRAM");
            SDL.SDL_Quit();
            return;
        }
        Console.WriteLine($"Selected microphone: {micName}");

        // Define audio spec values
        SDL.SDL_AudioSpec sDL_AudioSpecWant = new SDL.SDL_AudioSpec 
        {
            freq = 44100,               //sample rate in Hz
            format = SDL.AUDIO_F32,     //32bit float format, easier than int for FFT
            channels = 1,               //mono input
            samples = 4096,             //buffer size (approx 93ms of audio at 44100Hz)
            callback = null             //tell SDL to disable callbacks, using SDL_DequeueAudio instead (push method)
                                        //if null causes issues, figure out how to use IntPtr.Zero instead
        };

        // Audio spec values sdl finds
        SDL.SDL_AudioSpec sDL_AudioSpecHave; 

        // Open audio device for capture
        uint micDeviceID = SDL.SDL_OpenAudioDevice(micName,                     //name of mic
                                                   1,                           //playback = 0, caputure = 1;
                                                   ref sDL_AudioSpecWant,       //audio spec defined
                                                   out sDL_AudioSpecHave,       //audio spec received
                                                   0);                          //allowed changes by SDL (0 = none)

        if (micDeviceID == 0)
        {
            Console.WriteLine("Failed to open microphone: " + SDL.SDL_GetError());
            SDL.SDL_Quit();
            return;
        }

        Console.WriteLine($"Opened microphone: {micName}");
        Console.WriteLine($"Got format: {sDL_AudioSpecHave.freq} Hz, {sDL_AudioSpecHave.channels}");

        Console.WriteLine("Run Analysis? (y/n)");
        if (Console.ReadLine() == "y"){
            RunAnalysis(micDeviceID, sDL_AudioSpecHave);
        }
        else{
            SDL.SDL_Quit();
            Console.WriteLine("PROGRAM END");
            return;
        }

        
    }

    static string MicSelect()// Prompt user for microphone access
    {
        int micCount, userMicChoice;

        micCount = SDL.SDL_GetNumAudioDevices(iscapture: 1);//request recording devices using iscaputre>0
        if (micCount <= 0)
        {
            Console.WriteLine("No audio capture devices detected!");
            return "";
        }
        else
        {
            string[] micArr = new string[micCount];
            Console.WriteLine("Available audio capture devices:");
            for (int i = 0; i < micCount; i++)
            {
                string name = SDL.SDL_GetAudioDeviceName(i, 1);//args: index, iscapture
                micArr[i] = name;
                Console.WriteLine($"{i} : {name}");
            }
            Console.WriteLine($"Choose audio capture device (0-{micCount - 1}):");
            userMicChoice = int.Parse(Console.ReadLine() ?? "0");

            if (userMicChoice >= 0 && userMicChoice < micCount)
            {
                return micArr[userMicChoice];
            }
            else
            {
                return "";
            }
                
           
        }
    }

    static void RunAnalysis(uint micID, SDL.SDL_AudioSpec have) // Run audio analysis
    {
        SDL.SDL_PauseAudioDevice(micID, 0);

        Console.WriteLine("Recording audio... press any key to stop.");

        // Pull audio loop
        int floatSize = sizeof(float);
        uint bufferSamples = 4096; // Samples per pull
        float[] floatBuffer = new float[bufferSamples]; // Buffer to store float samples
        byte[] rawBuffer = new byte[bufferSamples * floatSize]; // Raw bytes from SDL

        

        while (!Console.KeyAvailable)
        {
            uint bytesAvailable = SDL.SDL_GetQueuedAudioSize(micID);
            if (bytesAvailable >= rawBuffer.Length) 
            {
                unsafe
                {
                    fixed (byte* ptr = rawBuffer)
                    {
                        //cannot pass managed byte[] directly into dequeue function, must convert to IntPtr
                        SDL.SDL_DequeueAudio(micID, (IntPtr)ptr, (uint)rawBuffer.Length); 
                    }
                }

                Buffer.BlockCopy(rawBuffer, 0, floatBuffer, 0, rawBuffer.Length);

                Complex[] fftBuffer = new Complex[floatBuffer.Length];
                for (int i = 0; i < floatBuffer.Length; i++)
                {
                    fftBuffer[i] = new Complex(floatBuffer[i], 0.0); // imaginary part = 0
                }
                // Perform FFT (in-place)
                Fourier.Forward(fftBuffer, FourierOptions.Matlab);

                double[] magnitudes = new double[fftBuffer.Length/2];
                for (int i = 0; i < magnitudes.Length; i++)
                {
                    magnitudes[i] = fftBuffer[i].Magnitude;
                }

                int peakIndex = 0;
                double peakValue = 0;

                for (int i = 0; i<magnitudes.Length; i++)
                {
                    if (magnitudes[i] > peakValue)
                    {
                        peakValue = magnitudes[i];
                        peakIndex = i;
                    }
                }

                //f = (K/N)*R
                double dominantFrequency = ((double)have.freq / fftBuffer.Length) * peakIndex;

                Console.WriteLine($"Dominant Frequency: {dominantFrequency:F1} Hz");

            }
            System.Threading.Thread.Sleep(50);// Delay to avoid tight CPU loop
        }

        Console.ReadKey(true);

        // Cleanup
        SDL.SDL_CloseAudioDevice(micID);
        SDL.SDL_Quit();
    }
}
