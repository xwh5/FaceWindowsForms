
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using Face.Sdk.ArcFace;
using Face.Sdk.FaceRecognitionDotNet;
using Face.Sdk.ViewFaceCodeSdk;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Face.ApplicationService.FaceService
{
    public class FaceService
    {
        private IFaceProvider faceProvider;
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
                case "ArcFace":
                    faceProvider = new ArcFaceProvider();
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
        public List<FaceDetectorDto> FaceDetector(Image image, out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceDetector(image);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }

        public bool FaceCompare(Bitmap img1, Bitmap img2, out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceCompare(img1, img2);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }
    }
}
