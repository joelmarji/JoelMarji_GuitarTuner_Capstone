using System;
using SDL2;

class Program
{
    static void Main()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_AUDIO) < 0)
        {
            Console.WriteLine($"SDL_Init Error: {SDL.SDL_GetError()}");
            return;
        }
        Console.WriteLine("SDL2 initialized successfully!");
        SDL.SDL_Quit();
    }
}
