namespace FaceWindowsForms
{
    partial class FaceRecognitionForm
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
            this.ButtonStart = new System.Windows.Forms.Button();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.videoDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new AForge.Controls.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonStart
            // 
            this.ButtonStart.Location = new System.Drawing.Point(938, 38);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(141, 64);
            this.ButtonStart.TabIndex = 1;
            this.ButtonStart.Text = "打开摄像头";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.Location = new System.Drawing.Point(27, 64);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(210, 132);
            this.videoSourcePlayer1.TabIndex = 2;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            // 
            // videoDeviceComboBox
            // 
            this.videoDeviceComboBox.FormattingEnabled = true;
            this.videoDeviceComboBox.Location = new System.Drawing.Point(958, 176);
            this.videoDeviceComboBox.Name = "videoDeviceComboBox";
            this.videoDeviceComboBox.Size = new System.Drawing.Size(121, 20);
            this.videoDeviceComboBox.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(920, 630);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // FaceRecognitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 654);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.videoDeviceComboBox);
            this.Controls.Add(this.videoSourcePlayer1);
            this.Controls.Add(this.ButtonStart);
            this.Name = "FaceRecognitionForm";
            this.Text = "人脸识别demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceRecognitionForm_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button ButtonStart;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.ComboBox videoDeviceComboBox;
        private AForge.Controls.PictureBox pictureBox1;
    }
}