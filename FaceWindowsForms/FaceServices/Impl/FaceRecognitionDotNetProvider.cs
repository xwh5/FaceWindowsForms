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
    public class FaceRecognitionDotNetProvider: FaceProvider,IDisposable
    {
        private FaceRecognition _FaceRecognition;
        public FaceRecognitionDotNetProvider() {
            var directory = Path.GetFullPath("Models"); 
            _FaceRecognition = FaceRecognition.Create(Directory.GetCurrentDirectory()+"Models");
        }
        public override List<FaceDetectorDto> FaceDetector(Bitmap image)
        {
            var result= _FaceRecognition.FaceLocations(FaceRecognition.LoadImage(image));
            return result.Select(r=>new FaceDetectorDto{ 
            
            }).ToList();
        }

        public override bool FaceCompare(Bitmap img1, Bitmap img2)
        {
            var cImg1 = FaceRecognition.LoadImage(img1);
            var cImg2 = FaceRecognition.LoadImage(img2);

            var c1 = _FaceRecognition.FaceEncodings(cImg1).First();
            var c2 = _FaceRecognition.FaceEncodings(cImg2).First();
            return FaceRecognition.CompareFace(c1,c2);
        }

        public void Dispose()
        {
            _FaceRecognition.Dispose();
        }
    }
}
