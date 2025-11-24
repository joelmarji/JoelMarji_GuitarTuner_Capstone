using System;
using SDL2;

namespace TunerCore
{
    public class AudioProcessor : IDisposable
    {
        private uint micDeviceID;
        private SDL.SDL_AudioSpec sDL_AudioSpecHave;
        private bool isInitialized = false;
        private bool isDeviceOpen = false;

        public SDL.SDL_AudioSpec AudioSpec => sDL_AudioSpecHave;

        // Initialize audio
        public AudioProcessor()
        {
            if (SDL.SDL_InitSubSystem(SDL.SDL_INIT_AUDIO) < 0)
            {
                isInitialized = false;
            }
            else
            {
                isInitialized = true;
            }
        }

        public bool OpenDevice(string micName)
        {
            if (!isInitialized)
            {
                return false;
            }

            // Define desired audio spec values
            SDL.SDL_AudioSpec sDL_AudioSpecWant = new SDL.SDL_AudioSpec
            {
                freq = 44100,               //sample rate in Hz
                format = SDL.AUDIO_F32,     //32-bit float format
                channels = 1,               //mono input
                samples = 4096,             //buffer size (approx 93ms of audio at 44100Hz)
                callback = null             //tell SDL to disable callbacks, using SDL_DequeueAudio instead (push method)
                                            //if null causes issues, figure out how to use IntPtr.Zero instead
            };

            micDeviceID = SDL.SDL_OpenAudioDevice(
                micName,
                1,
                ref sDL_AudioSpecWant, // Request microphone parameters from SDL
                out sDL_AudioSpecHave, // SDL returns available parameters
                0
            );

            if (micDeviceID == 0)
            {
                isDeviceOpen = false;
                return false;
            }

            isDeviceOpen = true;
            return true;
        }

        public string[] GetMicrophoneList()
        {
            if (!isInitialized)
            {
                return Array.Empty<string>();
            }

            int count = SDL.SDL_GetNumAudioDevices(iscapture: 1); //iscapture > 0 for recording devices
            if (count <= 0)
            {
                //no microphones found
                return Array.Empty<string>();
            }

            string[] names = new string[count];
            for (int i = 0; i < count; i++)
            {
                names[i] = SDL.SDL_GetAudioDeviceName(i, 1);
            }   

            return names;
        }

        // Pull audio samples from the microphone buffer 
        public float[] CaptureSamples(int bufferSamples)
        { 
            if (!isDeviceOpen)
            {
                return null;
            }

            SDL.SDL_PauseAudioDevice(micDeviceID, 0);

            int floatSize = sizeof(float);
            byte[] rawBuffer = new byte [bufferSamples * floatSize];

            //return null if not enough data is queued.
            if (SDL.SDL_GetQueuedAudioSize(micDeviceID) < rawBuffer.Length)
            {
                return null;
            }

            float[] floatBuffer = new float[bufferSamples];

            if (SDL.SDL_GetQueuedAudioSize(micDeviceID) >= rawBuffer.Length)
            {
                unsafe
                {
                    fixed (byte* ptr = rawBuffer)
                    {
                        SDL.SDL_DequeueAudio(micDeviceID, (IntPtr)ptr, (uint)rawBuffer.Length);
                    }
                }
                Buffer.BlockCopy(rawBuffer, 0, floatBuffer, 0, rawBuffer.Length);
            }
            return floatBuffer;
        }

        // Clean up audio resources on close
        public void Dispose()
        {
            if (micDeviceID != 0)
            {
                SDL.SDL_PauseAudioDevice(micDeviceID, 1); // Pause before closing
                SDL.SDL_CloseAudioDevice(micDeviceID);
                micDeviceID = 0;
            }

            if (isInitialized)
            {
                SDL.SDL_QuitSubSystem(SDL.SDL_INIT_AUDIO);
                SDL.SDL_Quit(); // Quit all of SDL
                isInitialized = false;
            }
        }
    }
}
