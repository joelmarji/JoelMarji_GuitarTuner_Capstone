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
            this.startBtn = new System.Windows.Forms.Button();
            this.micComboBox = new System.Windows.Forms.ComboBox();
            this.noteLbl = new System.Windows.Forms.Label();
            this.centsLbl = new System.Windows.Forms.Label();
            this.tunerNeedle = new System.Windows.Forms.ProgressBar();
            this.analysisTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(289, 411);
            this.startBtn.Margin = new System.Windows.Forms.Padding(2);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(126, 69);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // micComboBox
            // 
            this.micComboBox.FormattingEnabled = true;
            this.micComboBox.Location = new System.Drawing.Point(436, 26);
            this.micComboBox.Name = "micComboBox";
            this.micComboBox.Size = new System.Drawing.Size(288, 21);
            this.micComboBox.TabIndex = 1;
            this.micComboBox.SelectedIndexChanged += new System.EventHandler(this.micComboBox_SelectedIndexChanged);
            // 
            // noteLbl
            // 
            this.noteLbl.AutoSize = true;
            this.noteLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noteLbl.Location = new System.Drawing.Point(249, 222);
            this.noteLbl.Name = "noteLbl";
            this.noteLbl.Size = new System.Drawing.Size(39, 39);
            this.noteLbl.TabIndex = 2;
            this.noteLbl.Text = "--";
            this.noteLbl.Click += new System.EventHandler(this.noteLbl_Click);
            // 
            // centsLbl
            // 
            this.centsLbl.AutoSize = true;
            this.centsLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.centsLbl.Location = new System.Drawing.Point(364, 222);
            this.centsLbl.Name = "centsLbl";
            this.centsLbl.Size = new System.Drawing.Size(137, 39);
            this.centsLbl.TabIndex = 3;
            this.centsLbl.Text = "-- Cents";
            this.centsLbl.Click += new System.EventHandler(this.centsLbl_Click);
            // 
            // tunerNeedle
            // 
            this.tunerNeedle.Location = new System.Drawing.Point(237, 327);
            this.tunerNeedle.Name = "tunerNeedle";
            this.tunerNeedle.Size = new System.Drawing.Size(236, 23);
            this.tunerNeedle.TabIndex = 4;
            this.tunerNeedle.Click += new System.EventHandler(this.tunerNeedle_Click);
            // 
            // analysisTimer
            // 
            this.analysisTimer.Tick += new System.EventHandler(this.analysisTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 491);
            this.Controls.Add(this.tunerNeedle);
            this.Controls.Add(this.centsLbl);
            this.Controls.Add(this.noteLbl);
            this.Controls.Add(this.micComboBox);
            this.Controls.Add(this.startBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.ComboBox micComboBox;
        private System.Windows.Forms.Label noteLbl;
        private System.Windows.Forms.Label centsLbl;
        private System.Windows.Forms.ProgressBar tunerNeedle;
        private System.Windows.Forms.Timer analysisTimer;
    }
}

