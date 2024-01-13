using AForge.Controls;
using AForge.Video.DirectShow;
using Face.ApplicationService.FaceService;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViewFaceCore.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FaceWindowsForms
{
    public partial class FaceRecognitionForm : Form
    {
        /// <summary>
        /// 摄像头设备信息集合
        /// </summary>
        private FilterInfoCollection videoDevices;
        public FaceRecognitionForm()
        {
            InitializeComponent();
        }


        private void InitVideo()
        {
            videoSourcePlayer1.Visible = false;
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoDeviceComboBox.Items.Clear();
            videoDeviceComboBox.DataSource = null;
            foreach (FilterInfo info in videoDevices)
            {
                videoDeviceComboBox.Items.Add(info.Name);
            }
            if (videoDeviceComboBox.Items.Count > 0)
            {
                videoDeviceComboBox.SelectedIndex = 0;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            InitVideo();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ButtonStart.Text == "打开摄像头")
            {
                //开始
                try
                {
                    pictureBox1.Visible = true;
                    if (videoDeviceComboBox.SelectedIndex < 0)
                    {
                        MessageBox.Show($"没有找到可用的摄像头，请重试！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    FilterInfo info = videoDevices[videoDeviceComboBox.SelectedIndex];
                    VideoCaptureDevice videoCapture = new VideoCaptureDevice(info.MonikerString);
                    var videoResolution = videoCapture.VideoCapabilities.Where(p => p.FrameSize.Width == 1280 && p.FrameSize.Height == 720).FirstOrDefault();
                    if (videoResolution == null)
                    {
                        return;
                    }
                    videoCapture.VideoResolution = videoResolution;
                    videoSourcePlayer1.VideoSource = videoCapture;
                    videoSourcePlayer1.Start();
                    StartDetector();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ButtonStart.Text = "关闭摄像头";
                }
            }
            else if (ButtonStart.Text == "关闭摄像头")
            {
                Stop();
            }
        }
        private void Stop() {
            try
            {
                if (!videoSourcePlayer1.IsRunning)
                {
                    return;
                }
                videoSourcePlayer1?.SignalToStop();
                videoSourcePlayer1?.WaitForStop();
                pictureBox1.Visible = false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ButtonStart.Text = "打开摄像头";
            }
        }
        private async void StartDetector()
        {
            while (videoSourcePlayer1.IsRunning)
            {

                Bitmap bitmap = videoSourcePlayer1.GetCurrentVideoFrame();
                if (bitmap == null)
                {
                    await Task.Delay(10);
                    SetPic(bitmap);
                    continue;
                }
                var faceInfos = FaceGlobal.CurrentFaceService.FaceDetector(bitmap, out long ts);
                var name = FaceGlobal.CurrentFaceService.GetName(bitmap);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (faceInfos.Any()) // 如果有人脸，在 bitmap 上绘制出人脸的位置信息
                    {
                        g.DrawRectangles(new Pen(Color.Red, 4), faceInfos.Select(p => new RectangleF
                        {
                            X = p.x,
                            Y = p.y,
                            Width = p.width,
                            Height = p.height
                        }).ToArray());
                        for (int i = 0; i < faceInfos.Count; i++)
                        {
                            StringBuilder builder = new StringBuilder();
                            if (!string.IsNullOrEmpty(name))
                            {
                                builder.Append($"姓名: {name}");
                            }
                            else
                            {
                                builder.Append($"未从人脸库中找到");
                            }
                            g.DrawString(builder.ToString(), new Font("微软雅黑", 24), Brushes.Green, new PointF(faceInfos[i].x + faceInfos[i].width + 24, faceInfos[i].y));
                        }
                    }
                }

                await Task.Delay(30);
                SetPic(bitmap);
            }
        }

        private void SetPic(Bitmap bitmap)
        {
            pictureBox1.Invoke(new Action(() =>
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = bitmap;
            }));
        }
        private void FaceRecognitionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }
    }
}
