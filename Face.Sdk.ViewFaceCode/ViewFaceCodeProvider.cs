using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ViewFaceCore;
using ViewFaceCore.Core;
using ViewFaceCore.Model;

namespace Face.Sdk.ViewFaceCodeSdk
{
    public class ViewFaceCodeProvider : FaceProvider, IFaceFeature<float[]>
    {
        public bool Compare(float[] data, float[] dest)
        {
            using (var faceRecognizer= new FaceRecognizer())
            {
                return faceRecognizer.IsSelf(data, dest);
            }
        }

        public override void Dispose()
        {
        }

        public override bool FaceCompare(Image img1, Image img2)
        {
            FaceDetector faceDetector = null;
            FaceLandmarker faceMark = null;
            FaceRecognizer faceRecognizer = null;
            try
            {
                faceDetector = new FaceDetector();
                var face1 = faceDetector.Detect(img1.ToFaceImage());
                var face2 = faceDetector.Detect(img2.ToFaceImage());
                faceMark = new FaceLandmarker();
                FaceMarkPoint[] points0 = faceMark.Mark(img1, face1[0]);
                FaceMarkPoint[] points1 = faceMark.Mark(img2, face2[0]);
                //提取特征值
                faceRecognizer = new FaceRecognizer();
                float[] data0 = faceRecognizer.Extract(img1, points0);
                float[] data1 = faceRecognizer.Extract(img2, points1);
                //对比特征值
                bool isSelf = faceRecognizer.IsSelf(data0, data1);
                return isSelf;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                faceDetector.Dispose();
                faceMark.Dispose();
                faceRecognizer.Dispose();
            }
        }

        public override List<FaceDetectorDto> FaceDetector(System.Drawing.Image image)
        {
            using (FaceDetector faceDetector = new FaceDetector())
            {
                var result = faceDetector.Detect(image.ToFaceImage());
                return result.Select(r => new FaceDetectorDto
                {
                    Score = r.Score,
                    x=r.Location.X,
                    y=r.Location.Y,
                    height=r.Location.Height,
                    width=r.Location.Width,
                }).ToList();
            }
        }

        public float[] GetFeature(Image img1)
        {
            FaceDetector faceDetector = null;
            FaceLandmarker faceMark = null;
            FaceRecognizer faceRecognizer = null;
            try
            {
                faceDetector = new FaceDetector();
                var face1 = faceDetector.Detect(img1.ToFaceImage());
                faceMark = new FaceLandmarker();
                FaceMarkPoint[] points0 = faceMark.Mark(img1, face1[0]);
                //提取特征值
                faceRecognizer = new FaceRecognizer();
                float[] data0 = faceRecognizer.Extract(img1, points0);
                return data0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                faceDetector.Dispose();
                faceMark.Dispose();
                faceRecognizer.Dispose();
            }
        }
    }
}
