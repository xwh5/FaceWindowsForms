using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Face.ApplicationService.Share.FaceService
{
    public interface IBaseFaceLib
    {
        void InitFaceLib(string path);

        string Search(Image img);

        string Search(SKBitmap img);
    }
}
