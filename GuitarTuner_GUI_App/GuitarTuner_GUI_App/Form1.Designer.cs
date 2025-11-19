namespace GuitarTuner_GUI_App
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.startBtn = new System.Windows.Forms.Button();
            this.micComboBox = new System.Windows.Forms.ComboBox();
            this.noteLbl = new System.Windows.Forms.Label();
            this.centsLbl = new System.Windows.Forms.Label();
            this.tunerNeedleFlat = new System.Windows.Forms.ProgressBar();
            this.analysisTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tunerNeedleSharp = new System.Windows.Forms.ProgressBar();
            this.CheckImage = new System.Windows.Forms.PictureBox();
            this.xImage = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.freqAdjust = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CheckImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xImage)).BeginInit();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.Location = new System.Drawing.Point(396, 467);
            this.startBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(175, 85);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // micComboBox
            // 
            this.micComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.micComboBox.FormattingEnabled = true;
            this.micComboBox.Location = new System.Drawing.Point(579, 71);
            this.micComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.micComboBox.Name = "micComboBox";
            this.micComboBox.Size = new System.Drawing.Size(380, 33);
            this.micComboBox.TabIndex = 1;
            // 
            // noteLbl
            // 
            this.noteLbl.AutoSize = true;
            this.noteLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLbl.Location = new System.Drawing.Point(388, 245);
            this.noteLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.noteLbl.Name = "noteLbl";
            this.noteLbl.Size = new System.Drawing.Size(48, 48);
            this.noteLbl.TabIndex = 2;
            this.noteLbl.Text = "--";
            // 
            // centsLbl
            // 
            this.centsLbl.AutoSize = true;
            this.centsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.centsLbl.Location = new System.Drawing.Point(455, 245);
            this.centsLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.centsLbl.Name = "centsLbl";
            this.centsLbl.Size = new System.Drawing.Size(168, 48);
            this.centsLbl.TabIndex = 3;
            this.centsLbl.Text = "-- Cents";
            // 
            // tunerNeedleFlat
            // 
            this.tunerNeedleFlat.Cursor = System.Windows.Forms.Cursors.Default;
            this.tunerNeedleFlat.Location = new System.Drawing.Point(306, 324);
            this.tunerNeedleFlat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tunerNeedleFlat.MarqueeAnimationSpeed = 1;
            this.tunerNeedleFlat.Name = "tunerNeedleFlat";
            this.tunerNeedleFlat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tunerNeedleFlat.RightToLeftLayout = true;
            this.tunerNeedleFlat.Size = new System.Drawing.Size(150, 45);
            this.tunerNeedleFlat.TabIndex = 4;
            // 
            // analysisTimer
            // 
            this.analysisTimer.Tick += new System.EventHandler(this.analysisTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(682, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Microphone Select:";
            // 
            // tunerNeedleSharp
            // 
            this.tunerNeedleSharp.Location = new System.Drawing.Point(530, 324);
            this.tunerNeedleSharp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tunerNeedleSharp.MarqueeAnimationSpeed = 1;
            this.tunerNeedleSharp.Name = "tunerNeedleSharp";
            this.tunerNeedleSharp.Size = new System.Drawing.Size(150, 45);
            this.tunerNeedleSharp.TabIndex = 6;
            // 
            // CheckImage
            // 
            this.CheckImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CheckImage.BackgroundImage")));
            this.CheckImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("CheckImage.InitialImage")));
            this.CheckImage.Location = new System.Drawing.Point(463, 324);
            this.CheckImage.Name = "CheckImage";
            this.CheckImage.Size = new System.Drawing.Size(60, 55);
            this.CheckImage.TabIndex = 7;
            this.CheckImage.TabStop = false;
            // 
            // xImage
            // 
            this.xImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("xImage.BackgroundImage")));
            this.xImage.ErrorImage = null;
            this.xImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("xImage.InitialImage")));
            this.xImage.Location = new System.Drawing.Point(463, 324);
            this.xImage.Name = "xImage";
            this.xImage.Size = new System.Drawing.Size(60, 55);
            this.xImage.TabIndex = 8;
            this.xImage.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "A = ";
            // 
            // freqAdjust
            // 
            this.freqAdjust.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freqAdjust.Location = new System.Drawing.Point(62, 71);
            this.freqAdjust.Multiline = true;
            this.freqAdjust.Name = "freqAdjust";
            this.freqAdjust.Size = new System.Drawing.Size(77, 33);
            this.freqAdjust.TabIndex = 10;
            this.freqAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.freqAdjust.TextChanged += new System.EventHandler(this.freqAdjust_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(145, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Hz";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(62, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 49);
            this.button1.TabIndex = 12;
            this.button1.Text = "Enter";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.freqAdjust);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.xImage);
            this.Controls.Add(this.CheckImage);
            this.Controls.Add(this.tunerNeedleSharp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tunerNeedleFlat);
            this.Controls.Add(this.centsLbl);
            this.Controls.Add(this.noteLbl);
            this.Controls.Add(this.micComboBox);
            this.Controls.Add(this.startBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Guitar Tuner GUI App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CheckImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ComboBox micComboBox;
        private System.Windows.Forms.Label noteLbl;
        private System.Windows.Forms.Label centsLbl;
        private System.Windows.Forms.ProgressBar tunerNeedleFlat;
        private System.Windows.Forms.Timer analysisTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar tunerNeedleSharp;
        private System.Windows.Forms.PictureBox CheckImage;
        internal System.Windows.Forms.PictureBox xImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox freqAdjust;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}

