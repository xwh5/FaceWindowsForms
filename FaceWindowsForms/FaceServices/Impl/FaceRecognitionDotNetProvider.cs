using FaceRecognitionDotNet;
using FaceWindowsForms.FaceServices.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWindowsForms.FaceServices.Impl
{
    public class FaceRecognitionDotNetProvider : FaceProvider, IDisposable
    {
        private FaceRecognition _FaceRecognition;
        public FaceRecognitionDotNetProvider()
        {
            var directory = Path.GetFullPath("Models");
            _FaceRecognition = FaceRecognition.Create("Models");
        }
        public override List<FaceDetectorDto> FaceDetector(Bitmap image)
        {
            var result = _FaceRecognition.FaceLocations(FaceRecognition.LoadImage(image));
            return result.Select(r => new FaceDetectorDto
            {
                Score = r.Confidence
            }).ToList();
        }

        public override bool FaceCompare(Bitmap img1, Bitmap img2)
        {
            FaceRecognitionDotNet.Image cImg1 = null;
            FaceRecognitionDotNet.Image cImg2 = null;
            FaceEncoding c1 = null;
            FaceEncoding c2 = null;
            try
            {
                cImg1 = FaceRecognition.LoadImage(img1);
                cImg2 = FaceRecognition.LoadImage(img2);

                c1 = _FaceRecognition.FaceEncodings(cImg1).First();
                c2 = _FaceRecognition.FaceEncodings(cImg2).First();
                return FaceRecognition.CompareFace(c1, c2,0.4);
            }
            finally {
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
