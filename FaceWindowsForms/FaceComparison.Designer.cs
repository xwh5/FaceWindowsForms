namespace FaceWindowsForms
{
    partial class FaceComparison
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.logBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectImageToRegister = new System.Windows.Forms.Button();
            this.pictureBoxSelected = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBoxToRecognize = new System.Windows.Forms.PictureBox();
            this.btnSelectImageToRecognize = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelected)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxToRecognize)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(12, 252);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(1004, 186);
            this.logBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectImageToRegister);
            this.groupBox1.Controls.Add(this.pictureBoxSelected);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 234);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源图片";
            // 
            // btnSelectImageToRegister
            // 
            this.btnSelectImageToRegister.Location = new System.Drawing.Point(37, 26);
            this.btnSelectImageToRegister.Name = "btnSelectImageToRegister";
            this.btnSelectImageToRegister.Size = new System.Drawing.Size(132, 23);
            this.btnSelectImageToRegister.TabIndex = 5;
            this.btnSelectImageToRegister.Text = "选择图片";
            this.btnSelectImageToRegister.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSelectImageToRegister.UseVisualStyleBackColor = true;
            this.btnSelectImageToRegister.Click += new System.EventHandler(this.btnSelectImageToRegister_Click);
            // 
            // pictureBoxSelected
            // 
            this.pictureBoxSelected.Location = new System.Drawing.Point(37, 74);
            this.pictureBoxSelected.Name = "pictureBoxSelected";
            this.pictureBoxSelected.Size = new System.Drawing.Size(267, 143);
            this.pictureBoxSelected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSelected.TabIndex = 4;
            this.pictureBoxSelected.TabStop = false;
            this.pictureBoxSelected.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBoxSelected_LoadCompleted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBoxToRecognize);
            this.groupBox2.Controls.Add(this.btnSelectImageToRecognize);
            this.groupBox2.Location = new System.Drawing.Point(394, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 234);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标图片";
            // 
            // pictureBoxToRecognize
            // 
            this.pictureBoxToRecognize.Location = new System.Drawing.Point(22, 74);
            this.pictureBoxToRecognize.Name = "pictureBoxToRecognize";
            this.pictureBoxToRecognize.Size = new System.Drawing.Size(248, 143);
            this.pictureBoxToRecognize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxToRecognize.TabIndex = 2;
            this.pictureBoxToRecognize.TabStop = false;
            this.pictureBoxToRecognize.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBoxToRecognize_LoadCompleted);
            // 
            // btnSelectImageToRecognize
            // 
            this.btnSelectImageToRecognize.Location = new System.Drawing.Point(22, 26);
            this.btnSelectImageToRecognize.Name = "btnSelectImageToRecognize";
            this.btnSelectImageToRecognize.Size = new System.Drawing.Size(248, 23);
            this.btnSelectImageToRecognize.TabIndex = 1;
            this.btnSelectImageToRecognize.Text = "选择图片-->对比";
            this.btnSelectImageToRecognize.UseVisualStyleBackColor = true;
            this.btnSelectImageToRecognize.Click += new System.EventHandler(this.btnSelectImageToRecognize_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Location = new System.Drawing.Point(734, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 234);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "人脸库识别";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(16, 74);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(248, 143);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "识别";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 21);
            this.textBox1.TabIndex = 5;
            // 
            // FaceComparison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logBox);
            this.Name = "FaceComparison";
            this.Text = "人脸对比demo";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelected)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxToRecognize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectImageToRegister;
        private System.Windows.Forms.PictureBox pictureBoxSelected;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelectImageToRecognize;
        private System.Windows.Forms.PictureBox pictureBoxToRecognize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

