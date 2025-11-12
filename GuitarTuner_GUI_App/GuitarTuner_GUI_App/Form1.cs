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

            tunerNeedle.Minimum = 0;
            tunerNeedle.Maximum = 50;
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

        private void micComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void noteLbl_Click(object sender, EventArgs e)
        {

        }

        private void centsLbl_Click(object sender, EventArgs e)
        {

        }

        private void tunerNeedle_Click(object sender, EventArgs e)
        {

        }

        private void analysisTimer_Tick(object sender, EventArgs e)
        {
            float[] samples = audio.CaptureSamples(4096);
            if (samples == null || samples.Length == 0)
            {
                return;
            }

            TuningResult result = analyzer.GetNote(samples);
            if (result.isValid)
            {
                noteLbl.Text = result.NoteName;
                string sign = result.CentsDeviation >= 0 ? "+" : "";
                centsLbl.Text = $"{sign}{result.CentsDeviation} cents";

                tunerNeedle.Value = Math.Max(tunerNeedle.Minimum, Math.Min(tunerNeedle.Maximum , result.CentsDeviation));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dispose audio resources
            if (audio != null)
            {
                audio.Dispose();
            }
        }
    }
}
