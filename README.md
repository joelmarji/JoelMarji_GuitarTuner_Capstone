# C# Chromatic Instrument Tuner

A real-time, chromatic instrument tuner built with C# and Windows Forms. This application uses advanced signal processing techniques (FFT, HPS, Hann Window, Quadratic Interpolation) to detect musical notes with high precision, filtering out background noise and harmonic overtones.

## Features

* **Real-Time Audio Analysis:** Captures low-latency audio using SDL2.
* **Chromatic Tuning:** Detects note and octave of frequency being played
* **Visual Feedback:**
    * **Needle Indicators:** Dual progress bars visualize how Flat (Left) or Sharp (Right) the note is.
    * **Cents Deviation:** Displays the tuning error in cents.
    * **Lock-on UI:** Visual "Checkmark" or "X" indicates when the note is in tune (±5 cents).
* **Adjustable Concert Pitch:** Change the A4 reference frequency (default 440Hz) to support alternative standards (e.g., 432Hz).
* **Microphone Selection:** Dropdown menu to select specific audio input devices.

## Technology Stack

* **Language:** C# (.NET)
* **UI Framework:** Windows Forms (WinForms)
* **Audio Capture:** [SDL2](https://www.libsdl.org/) (via SDL2-CS wrapper)
* **Math Library:** [MathNet.Numerics](https://numerics.mathdotnet.com/)

## Signal Processing Pipeline

This tuner implements a 4-stage processing pipeline to ensure accuracy:

1.  **Hann Windowing:**
    * Raw audio samples are processed with a Hann window function to smooth the edges of the buffer to reduce spectral leakage.
2.  **Fast Fourier Transform (FFT):**
    * Converts the time-domain audio signal into the frequency domain using MathNet.Numerics, creating a spectrum of frequency magnitudes.
3.  **Harmonic Product Spectrum (HPS):**
    * The algorithm multiplies the spectrum by down-sampled versions of itself. This amplifies the Fundamental Frequency and suppresses harmonics (overtones), preventing the tuner from accidentally locking onto an octave too high.
    * If the signal is too weak for HPS, it  falls back to raw peak detection to maintain sensitivity.
4.  **Quadratic Interpolation:**
    * Since FFT bins are discrete (approx. 10Hz wide), the true peak often lies between two bins. Quadratic Interpolation fits a parabola to the peak and its neighbors to calculate the fractional offset, improving precision to ~0.1Hz.

## Project Structure

The solution is split into a modular Core library and a GUI frontend:

* **`TunerCore/`**
    * `AudioProcessor.cs`: Handles SDL2 initialization, device enumeration, and raw buffer capture.
    * `FFTAnalyzer.cs`: Contains the math pipeline (Windowing, FFT, HPS, Note mapping logic).
* **`GuitarTuner_GUI_App/`**
    * `Form1.cs`: Handles UI logic, timers, animations, and user input.
* **`GuitarTuner_Console_App/`**
    * Simple text based UI for debugging/demonstration

## Setup & Installation

### Prerequisites
* Visual Studio 2019 or later.
* .NET Framework / .NET Core (depending on your specific target).

### Handling SDL2.dll
This project relies on the unmanaged `SDL2.dll`.

1.  Clone the repository.
2.  Ensure `SDL2.dll` is present in your project.
3.  **Critical:** In Visual Studio, set the `SDL2.dll` property **"Copy to Output Directory"** to `Copy if newer`.
4.  **Architecture:** Ensure your build target matches the DLL version:
    * If using **32-bit SDL2.dll**, set Build Target to **x86**.
    * If using **64-bit SDL2.dll**, set Build Target to **x64**.

### Building
1.  Open the Solution in Visual Studio.
2.  Restore NuGet packages (MathNet.Numerics).
3.  Press **Start** to run.

## Troubleshooting

* **`DllNotFoundException: Unable to load DLL 'SDL2'`**:
    * The `SDL2.dll` is not in the `bin/Debug` (or `bin/Release`) folder. Check the "Copy to Output Directory" property.
* **`BadImageFormatException`**:
    * You are trying to run a 64-bit app with a 32-bit DLL (or vice versa). Go to Project Properties -> Build -> Platform Target and switch it to **x86** (or match your DLL).

## License
[MIT License]
