using System;
using SDL2;

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
            RunAnalysis(micDeviceID);
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

    static void RunAnalysis(uint micID) // Run audio analysis
    {
        SDL.SDL_PauseAudioDevice(micID, 0);

        Console.WriteLine("Recording audio... press any key to stop.");

        // Pull audio loop
        int floatSize = sizeof(float);
        uint bufferSamples = 4096; // Samples per pull
        float[] floatBuffer = new float[bufferSamples]; // Buffer to store float samples
        byte[] rawBuffer = new byte[bufferSamples * floatSize]; // Raw bytes from SDL
        uint bytesToRead = (uint)rawBuffer.Length; //cast as uint 

        

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
                        SDL.SDL_DequeueAudio(micID, (IntPtr)ptr, bytesToRead); 
                    }
                }

                Buffer.BlockCopy(rawBuffer, 0, floatBuffer, 0, rawBuffer.Length);

                Console.Write("Samples: ");
                for (int i = 0; i<5; i++)
                {
                    Console.Write($"{floatBuffer[i]:F3}");
                }
                Console.WriteLine();
            }
            System.Threading.Thread.Sleep(50);// Delay to avoid tight CPU loop
        }

        Console.ReadKey(true);

        // Cleanup
        SDL.SDL_CloseAudioDevice(micID);
        SDL.SDL_Quit();
    }
}
