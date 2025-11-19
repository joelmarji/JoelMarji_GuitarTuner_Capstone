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
            ((System.ComponentModel.ISupportInitialize)(this.CheckImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xImage)).BeginInit();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(406, 466);
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
            this.micComboBox.FormattingEnabled = true;
            this.micComboBox.Location = new System.Drawing.Point(579, 71);
            this.micComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.micComboBox.Name = "micComboBox";
            this.micComboBox.Size = new System.Drawing.Size(380, 24);
            this.micComboBox.TabIndex = 1;
            // 
            // noteLbl
            // 
            this.noteLbl.AutoSize = true;
            this.noteLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLbl.Location = new System.Drawing.Point(398, 244);
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
            this.centsLbl.Location = new System.Drawing.Point(465, 244);
            this.centsLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.centsLbl.Name = "centsLbl";
            this.centsLbl.Size = new System.Drawing.Size(168, 48);
            this.centsLbl.TabIndex = 3;
            this.centsLbl.Text = "-- Cents";
            // 
            // tunerNeedleFlat
            // 
            this.tunerNeedleFlat.Location = new System.Drawing.Point(316, 323);
            this.tunerNeedleFlat.Margin = new System.Windows.Forms.Padding(4);
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
            this.label1.Location = new System.Drawing.Point(715, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Microphone Select:";
            // 
            // tunerNeedleSharp
            // 
            this.tunerNeedleSharp.Location = new System.Drawing.Point(540, 323);
            this.tunerNeedleSharp.Margin = new System.Windows.Forms.Padding(4);
            this.tunerNeedleSharp.Name = "tunerNeedleSharp";
            this.tunerNeedleSharp.Size = new System.Drawing.Size(150, 45);
            this.tunerNeedleSharp.TabIndex = 6;
            // 
            // CheckImage
            // 
            this.CheckImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CheckImage.BackgroundImage")));
            this.CheckImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("CheckImage.InitialImage")));
            this.CheckImage.Location = new System.Drawing.Point(473, 323);
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
            this.xImage.Location = new System.Drawing.Point(473, 323);
            this.xImage.Name = "xImage";
            this.xImage.Size = new System.Drawing.Size(60, 55);
            this.xImage.TabIndex = 8;
            this.xImage.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
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
            this.Text = "Form1";
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
    }
}

