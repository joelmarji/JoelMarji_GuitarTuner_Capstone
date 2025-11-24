using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TunerCore;

namespace GuitarTuner_GUI_App
{
    public partial class Form1 : Form
    {
        private AudioProcessor audio;
        private FFTAnalyzer analyzer;
        public Form1()
        {
            InitializeComponent();
            int userFreq = 440;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            audio = new AudioProcessor();
            string[] mics = audio.GetMicrophoneList();

            if (mics.Length == 0)
            {
                MessageBox.Show("No audio capture devices detected!");
                startBtn.Enabled = false;
                return;
            }

            micComboBox.Items.AddRange(mics);
            micComboBox.SelectedIndex = 0;

            analysisTimer.Interval = 50;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (analysisTimer.Enabled)
            {
                analysisTimer.Stop();
                startBtn.Text = "Start";
            }
            else
            {
                string selectedMic = micComboBox.SelectedItem.ToString();
                if (!audio.OpenDevice(selectedMic))
                {
                    MessageBox.Show("Failed to open selected microphone.");
                    return;
                }
                analyzer = new FFTAnalyzer(audio.AudioSpec.freq);
                analysisTimer.Start();
                startBtn.Text = "Stop";
            }
        }

        private void analysisTimer_Tick(object sender, EventArgs e)
        {
            float[] samples = audio.CaptureSamples(4096); // Collect audio sample buffer
            if (samples == null || samples.Length == 0)
            {
                return;
            }

            TuningResult result = analyzer.GetNote(samples); // Pass in audio samples to be processed
            if (result.isValid)
            {
                noteLbl.Text = result.NoteName;
                string sign = result.CentsDeviation >= 0 ? "+" : "";
                centsLbl.Text = $"{sign}{result.CentsDeviation} cents";
                // Set icon
                if (result.CentsDeviation > 5 || result.CentsDeviation < -5)
                {
                    CheckImage.Visible = false;
                    xImage.Visible = true;
                }
                else
                {
                    CheckImage.Visible = true;
                    xImage.Visible = false;
                }

                tunerNeedleFlat.Value = 0;
                tunerNeedleSharp.Value = 0;

                int deviation = result.CentsDeviation;

                if (deviation < 0)
                {
                    tunerNeedleFlat.Value = Math.Min(tunerNeedleFlat.Maximum, Math.Abs(deviation));
                }
                else if (deviation > 0)
                {
                    tunerNeedleSharp.Value = Math.Min(tunerNeedleSharp.Maximum, deviation);
                }
            }
        }

        //TODO:
        // Finish this feature 
        private void freqAdjust_TextChanged(object sender, EventArgs e)
        {
            string f = freqAdjust.Text;
            if (int.TryParse(f, out int userFreq))
            {
                userFreq = Math.Max(400, Math.Min(480, userFreq)); //clamp between 400 and 480
                freqAdjust.Text = f;
            }
            else
            {
                //invalid input, reset to 440
                freqAdjust.Text = "440";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose audio resources
            if (audio != null)
            {
                audio.Dispose();
            }
        }
    }
}
