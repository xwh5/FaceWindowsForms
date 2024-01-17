using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Face.ApplicationService.Share
{
    public interface IFaceFeature<T>
    {
        T GetFeature(Image img1);

        T GetFeature(SKBitmap img1);

        bool Compare(T data,T dest);
    }
}
