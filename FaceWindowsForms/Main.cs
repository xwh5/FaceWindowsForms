using Face.ApplicationService.FaceService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceWindowsForms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FaceRecognitionForm form1 = new FaceRecognitionForm();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FaceComparison form1 = new FaceComparison();
            form1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FaceGlobal.Key = FaceGlobal.ComboBox1Data[comboBox1.SelectedIndex];
            if (FaceGlobal.CurrentFaceService !=null)
            {
                FaceGlobal.CurrentFaceService.Dispose();
            }
            FaceGlobal.CurrentFaceService = new FaceService(FaceGlobal.Key,FaceGlobal.FaceLibPath);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(FaceGlobal.ComboBox1Data);
            comboBox1.SelectedItem = FaceGlobal.ComboBox1Data[0];
            FaceGlobal.Key = FaceGlobal.ComboBox1Data[0];
        }
    }
}
