using FaceWindowsForms.FaceServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Face.ViewFaceCode
{
    public class ViewFaceCodeProvider : FaceProvider
    {
        public override bool ComparePicture(Image img1, Image img2)
        {
            throw new NotImplementedException();
        }

        public override List<FaceDetectorDto> FaceDetector(Image image)
        {
            var bitmap = new Bitmap(image);
            using (FaceDetector faceDetector = new FaceDetector())
            {
                var result = faceDetector.Detect(bitmap.ToFaceImage());
                return result.Select(r => new FaceDetectorDto
                {
                    Score = r.Score,
                }).ToList();
            }
        }
    }
}
