using Face.ApplicationService.Share.FaceService.Dto;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Face.ApplicationService.Share.FaceService
{
    public interface IFaceProvider
    {
        List<FaceDetectorDto> FaceDetector(Image image);
        bool FaceCompare(Image img1, Image img2);
    }
}
