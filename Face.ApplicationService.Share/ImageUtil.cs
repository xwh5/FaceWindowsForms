using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Face.ApplicationService.Share
{
    public static class ImageUtil
    {
        public static Stream GetImageStream(Image image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            stream.Position = 0; // 重置流的位置
            return stream;
        }

    }
}
