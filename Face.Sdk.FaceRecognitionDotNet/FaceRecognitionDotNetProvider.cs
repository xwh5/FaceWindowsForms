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
    public class FaceRecognitionDotNetProvider : IFaceProvider, IDisposable
    {
        private FaceRecognition _FaceRecognition;
        public FaceRecognitionDotNetProvider()
        {
            _FaceRecognition = FaceRecognition.Create("Models");
        }
        public List<FaceDetectorDto> FaceDetector(System.Drawing.Image image)
        {

            var result = _FaceRecognition.FaceLocations(FaceRecognition.LoadImage(new Bitmap(image)));
            return result.Select(r => new FaceDetectorDto
            {
                Score = r.Confidence
            }).ToList();
        }

        public bool FaceCompare(System.Drawing.Image img1, System.Drawing.Image img2)
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
                return FaceRecognition.CompareFace(c1, c2, 0.6);
            }
            finally
            {
                cImg1.Dispose();
                cImg2.Dispose();
                c1.Dispose();
                c2.Dispose();
            }
        }


        public void Dispose()
        {
            _FaceRecognition.Dispose();
        }
    }
}
