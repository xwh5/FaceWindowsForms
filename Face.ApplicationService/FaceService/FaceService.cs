
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
using System.IO;

namespace Face.ApplicationService.FaceService
{
    public class FaceService:IDisposable
    {
        private IFaceProvider faceProvider;
        private IBaseFaceLib faceLib;
        public FaceService(string key,string path,bool isAction=true)
        {
            InitProvider(key, isAction);
            InitFaceLib(key, path);
        }

        private void InitFaceLib(string key,string path) {
            switch (key)
            {
                case "ViewFaceCode":
                    faceLib = new ViewFaceCodeFaceLib(faceProvider,key);
                    break;
                case "FaceRecognitionDotNet":
                    faceLib = new FaceRecognitionDotNetFaceLib(faceProvider, key);
                    break;
                case "ArcFace":
                    faceLib = new ArcFaceFaceLib(faceProvider, key);
                    break;
                case "Opencv":
                    faceLib = new OpencvSharpFaceLib(faceProvider, key);
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (Directory.Exists(path))
            {
                faceLib.InitFaceLib(path);
            }
        }
        private void InitProvider(string key,bool isAction)
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
                    faceProvider = new ArcFaceProvider(isAction);
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
        public List<FaceDetectorDto> FaceDetector(System.Drawing.Image image, out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceDetector(image);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }

        
        public List<FaceDetectorDto> FaceDetector(SKBitmap image)
        {
            var result = faceProvider.FaceDetector(image);

            return result;
        }

        public bool FaceCompare(System.Drawing.Image img1, System.Drawing.Image img2, out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceCompare(img1, img2);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }

        public bool FaceCompare(SKBitmap img1, SKBitmap img2, out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceProvider.FaceCompare(img1, img2);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }
        public string GetName(System.Drawing.Image img,out long ts) {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceLib.Search(img);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;
        }
        public string GetName(SKBitmap img,out long ts)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = faceLib.Search(img);
            ts = sw.ElapsedMilliseconds;
            sw.Stop();
            return result;

        }

        public void Dispose()
        {
            faceProvider.Dispose();

        }
    }
}
