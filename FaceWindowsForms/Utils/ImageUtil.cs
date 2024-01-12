using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace FaceWindowsForms.Utils
{
    internal static class ImageUtil
    {
        public static Image readFromFile(string imageUrl)
        {
            Image img = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(imageUrl, FileMode.Open, FileAccess.Read);
                img = Image.FromStream(fs);
            }
            finally
            {
                fs.Close();
            }
            return img;
        }

        public static Stream GetImageStream(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            ms.Position = 0; // 重置流的位置
            return ms;
        }
        public static SKBitmap ToSKBitmap(this Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                // 将 System.Drawing.Image 保存到内存流中
                image.Save(memoryStream, image.RawFormat);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // 从内存流中创建 SKBitmap
                using (var skStream = new SKManagedStream(memoryStream))
                {
                    return SKBitmap.Decode(skStream);
                }
            }
        }

    }
}
