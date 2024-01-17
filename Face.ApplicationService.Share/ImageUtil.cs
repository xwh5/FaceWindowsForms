using Org.BouncyCastle.Utilities;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace Face.ApplicationService.Share
{
    public static class ImageUtil
    {

        public static Stream GetImageStream(Image image)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            stream.Position = 0; // 重置流的位置
            return stream;
        }

        public static Stream GetImageStream(SKBitmap skBitmap)
        {
            // 将SKBitmap转换为SKImage
            using (var skImage = SKImage.FromBitmap(skBitmap))
            {
                // 对SKImage进行编码
                var encodedData = skImage.Encode();
                // 将SKData转换为Stream
                return encodedData.AsStream();
            }
        }

        public static byte[] ArrayToByte(this double[] doubles)
        {
            byte[] bytes = new byte[doubles.Length * sizeof(double)];
            Buffer.BlockCopy(doubles, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static double[] ByteToArray(this byte[] bytes)
        {
            double[] doubles = new double[bytes.Length / sizeof(double)];
            Buffer.BlockCopy(bytes, 0, doubles, 0, bytes.Length);
            return doubles;
        }
    }
}
