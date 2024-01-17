using Face.ApplicationService.Share.FaceService.Dto;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Face.ApplicationService.Share.FaceService
{
    public class FaceProvider : IFaceProvider
    {
        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual bool FaceCompare(Image img1, Image img2)
        {
            throw new NotImplementedException();
        }

        public virtual List<FaceDetectorDto> FaceDetector(Image image)
        {
            throw new NotImplementedException();
        }

        public virtual bool FaceCompare(SKBitmap img1, SKBitmap img2)
        {
            throw new NotImplementedException();
        }

        public virtual List<FaceDetectorDto> FaceDetector(SKBitmap sKBitmap)
        {
            throw new NotImplementedException();
        }
    }
}
