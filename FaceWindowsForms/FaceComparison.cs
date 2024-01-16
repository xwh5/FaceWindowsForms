using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using Face.ApplicationService.FaceService;
using FaceWindowsForms.Utils;
using SkiaSharp;

namespace FaceWindowsForms
{
    public partial class FaceComparison : Form
    {
        public static Form form1;
        /// <summary>
        /// 图片最大大小
        /// </summary>
        private long maxSize = 1024 * 1024 * 5;
        /// <summary>
        /// 默认打开文件夹路径
        /// </summary>
        private string InitialDirectoryPath = @"D:\Data\WXWork\1688856040371791\Cache\File\2024-01\dir_011_1";

        /// <summary>
        /// 
        /// </summary>
        //private string provideKey = "FaceRecognitionDotNet";
        //private string provideKey = "ViewFaceCode";

        private new string[] comboBox1Data = new string[] {
            "ViewFaceCode","FaceRecognitionDotNet","ArcFace"
        };

        public FaceComparison()
        {
            InitializeComponent();
            form1 = this;
            AppendText("当前人脸识别组件：" + FaceGlobal.Key);
            InitFaceService();
        }

        public void InitFaceService()
        {
            logBox.Text = null;
            AppendText($"当前识别引擎：{FaceGlobal.Key}");
            //FaceGlobal.CurrentFaceService = new FaceService(FaceGlobal.Key);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRegisterFace_Click(object sender, EventArgs e)
        {

        }
        public void AppendText(string message)
        {
            logBox.AppendText(message + Environment.NewLine);
        }
        private bool checkImage(string imagePath)
        {
            if (imagePath == null)
            {
                AppendText("Image not existed\r\r\n");
                return false;
            }
            try
            {
                //判断图片是否正常，如将其他文件把后缀改为.jpg，这样就会报错
                Image image = ImageUtil.readFromFile(imagePath);
                if (image == null)
                {
                    throw new Exception();
                }
                else
                {
                    image.Dispose();
                }
            }
            catch
            {
                AppendText(string.Format("{0} Bad image format\r\r\n", imagePath));
                return false;
            }
            FileInfo fileCheck = new FileInfo(imagePath);
            if (fileCheck.Exists == false)
            {
                AppendText(string.Format("{0} not existed\r\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length > maxSize)
            {
                AppendText(string.Format("{0} File size larger than 2MB\r\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length < 2)
            {
                AppendText(string.Format("{0} File size too small\r\r\n", fileCheck.Name));
                return false;
            }
            return true;
        }

        private void btnSelectImageToRegister_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "目标图片";
            openFileDialog.Filter = "Image File|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.FileName = string.Empty;
            openFileDialog.InitialDirectory = InitialDirectoryPath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                if (!checkImage(fileName))
                    return;
                //if (pictureBoxSelected.Image!=null)
                //{
                //    pictureBoxSelected.Image.Dispose();
                //}
                pictureBoxSelected.ImageLocation = fileName;
            }

        }

        private void btnSelectImageToRecognize_Click(object sender, EventArgs e)
        {
            if (pictureBoxSelected.Image is null)
            {
                AppendText("请先选择目标图片");
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "目标图片";
            openFileDialog.Filter = "Image File|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.InitialDirectory = InitialDirectoryPath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                if (!checkImage(fileName))
                    return;
                //if (pictureBoxSelected.Image != null)
                //{
                //    pictureBoxSelected.Image.Dispose();
                //}
                pictureBoxToRecognize.ImageLocation = fileName;

            }
        }
        private void ComparePicture(Image img1, Image img2)
        {
            AppendText("开始对比");
            var faceInfo = FaceGlobal.CurrentFaceService.FaceCompare(img1, img2, out long ts);
            AppendText($"是否为同一人脸{faceInfo},耗时：{ts}");
        }

        private void pictureBoxSelected_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            logBox.Text = null;
            var faceInfo = FaceGlobal.CurrentFaceService.FaceDetector(pictureBoxSelected.Image, out long ts);
            if (faceInfo.Count <= 0)
            {
                pictureBoxSelected.Image = null;
                AppendText($"未识别到人脸，请重新选择图片");
            }
            else
            {
                AppendText($"识别到人脸，个数：{faceInfo.Count},耗时：{ts}");
            }
        }

        private void pictureBoxToRecognize_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ComparePicture(pictureBoxSelected.Image, pictureBoxToRecognize.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = FaceGlobal.CurrentFaceService.GetName(pictureBoxSelected.Image, out long ts);
            AppendText($"人脸库查找人脸耗时：{ts}");
            if (string.IsNullOrWhiteSpace(name))
            {
                textBox1.Text = $"为匹配到人脸";
            }
            else
            {
                textBox1.Text = name;
            };
        }
    }
}
