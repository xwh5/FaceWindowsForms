using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Image = FaceRecognitionDotNet.Image;

namespace Face.Sdk.FaceRecognitionDotNet
{
    public class FaceRecognitionDotNetProvider : FaceProvider, IFaceFeature<FaceEncoding>
    {
        private FaceRecognition _FaceRecognition;
        public FaceRecognitionDotNetProvider()
        {
            _FaceRecognition = FaceRecognition.Create("Models");
        }
        public override List<FaceDetectorDto> FaceDetector(System.Drawing.Image image)
        {
            var result = _FaceRecognition.FaceLocations(FaceRecognition.LoadImage(new Bitmap(image)));
            return result.Select(r => new FaceDetectorDto
            {
                Score = r.Confidence,
                x = r.Left,
                y = r.Top,
                height = r.Right,
                width = r.Bottom
            }).ToList();
        }

        public override bool FaceCompare(System.Drawing.Image img1, System.Drawing.Image img2)
        {
            Image cImg1 = null;
            Image cImg2 = null;
            FaceEncoding c1 = null;
            FaceEncoding c2 = null;
            try
            {
                cImg1 = FaceRecognition.LoadImage(new Bitmap(img1));
                cImg2 = FaceRecognition.LoadImage(new Bitmap(img2));

                c1 = _FaceRecognition.FaceEncodings(cImg1).First();
                c2 = _FaceRecognition.FaceEncodings(cImg2).First();
                return FaceRecognition.CompareFace(c1, c2, 0.5);
            }
            finally
            {
                cImg1.Dispose();
                cImg2.Dispose();
                c1.Dispose();
                c2.Dispose();
            }
        }


        public override void Dispose()
        {
            _FaceRecognition.Dispose();
        }


        FaceEncoding IFaceFeature<FaceEncoding>.GetFeature(System.Drawing.Image img1)
        {
            using (var i = new Bitmap(img1))
            {
                using (var cImg1 = FaceRecognition.LoadImage(i))
                {
                    return _FaceRecognition.FaceEncodings(cImg1)?.FirstOrDefault();
                };
            }

        }

        public bool Compare(FaceEncoding data, FaceEncoding dest)
        {
            return FaceRecognition.CompareFace(data, dest, 0.4);
        }
    }
}
