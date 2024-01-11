using FaceWindowsForms.FaceServices.Dto;
using FaceWindowsForms.FaceServices.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FaceWindowsForms.FaceServices
{
    public class FaceService
    {
        private FaceProvider faceProvider;
        public FaceService(string key)
        {
            InitProvider(key);
        }
        private void InitProvider(string key)
        {
            switch (key)
            {
                case "ViewFaceCode":
                    faceProvider = new ViewFaceCodeProvider();
                    break;
                case "FaceRecognitionDotNet":
                    faceProvider = new FaceRecognitionDotNetProvider();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public List<FaceDetectorDto> FaceDetector(Bitmap image,out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result= faceProvider.FaceDetector(image);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }

        public bool FaceCompare(Bitmap img1, Bitmap img2,out long ts) 
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceCompare(img1, img2);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }
    }
}
