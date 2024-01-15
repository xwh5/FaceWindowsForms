
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using Face.Sdk.ArcFace;
using Face.Sdk.FaceRecognitionDotNet;
using Face.Sdk.OpencvSharpV4;
using Face.Sdk.ViewFaceCode;
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
        private IBaseFaceLib faceLib;
        public FaceService(string key,string path)
        {
            InitProvider(key);
            InitFaceLib(key, path);
        }

        private void InitFaceLib(string key,string path) {
            switch (key)
            {
                case "ViewFaceCode":
                    faceLib = new ViewFaceCodeFaceLib(faceProvider);
                    break;
                case "FaceRecognitionDotNet":
                    faceLib = new FaceRecognitionDotNetFaceLib(faceProvider);
                    break;
                case "ArcFace":
                    faceLib = new ArcFaceFaceLib(faceProvider );
                    break;
                case "Opencv":
                    faceLib = new OpencvSharpFaceLib(faceProvider);
                    break;
                default:
                    throw new NotImplementedException();
            }
            faceLib.InitFaceLib(path);
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
                case "Opencv":
                    faceProvider = new OpencvSharpProvider();
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

        public bool FaceCompare(Image img1, Image img2, out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceCompare(img1, img2);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }
        public string GetName(Image img) {
            return faceLib.Search(img);
        }
    }
}
