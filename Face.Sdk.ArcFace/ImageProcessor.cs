using Face.Sdk.ArcFace.Extensions;
using Face.Sdk.ArcFace.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Face.Sdk.ArcFace
{
    public class ImageProcessor : IImageProcessor
    {
        public async Task<string> GetFormatAsync(Stream imageStream)
        {
            var memoryStream = new MemoryStream();
            await imageStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // 重置流的位置

            // 使用SKCodec获取图像格式
            using (var codec = SKCodec.Create(memoryStream))
            {
                var format = codec.EncodedFormat.ToString().ToLower();

                return format; // 返回图像格式的小写字符串
            }
        }

        public async Task<ImageInfo> GetImageInfoAsync(Stream image)
        {
            byte[] imageData;
            using (var ms = new MemoryStream())
            {
                await image.CopyToAsync(ms);
                imageData = ms.ToArray();
            }

            var bitmap = SKBitmap.Decode(imageData);
            var sourceBitArrayLength = bitmap.Width * bitmap.Height * 3;
            var sourceBitArray = new byte[sourceBitArrayLength];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    var index = (y * bitmap.Width + x) * 3;
                    sourceBitArray[index] = color.Red;
                    sourceBitArray[index + 1] = color.Green;
                    sourceBitArray[index + 2] = color.Blue;
                }
            }

            var imageInfo = new ImageInfo
            {
                Width = bitmap.Width,
                Height = bitmap.Height,
                Format = AsfImagePixelFormat.ASVL_PAF_RGB24_B8G8R8,
                ImgData = Marshal.AllocHGlobal(sourceBitArrayLength)
            };
            Marshal.Copy(sourceBitArray, 0, imageInfo.ImgData, sourceBitArrayLength);
            return imageInfo;
        }





        public async Task<ImageInfo> GetIrImageInfoAsync(Stream imageStream)
        {
            // 从Stream中加载图片
            SKBitmap skBitmap;
            using (var managedStream = new SKManagedStream(imageStream))
            {
                skBitmap = SKBitmap.Decode(managedStream);
            }

            // 获取图片的宽度和高度
            int width = skBitmap.Width;
            int height = skBitmap.Height;

            // 计算源字节数组长度
            int sourceBitArrayLength = width * height * 3; // RGB24格式每个像素3个字节
            byte[] sourceBitArray = skBitmap.Bytes;

            // 创建灰度字节数组
            int destBitArrayLength = width * height;
            byte[] destBitArray = new byte[destBitArrayLength];

            // 灰度化处理
            int j = 0;
            for (int i = 0; i < sourceBitArray.Length; i += 3)
            {
                var colorTemp = sourceBitArray[i + 2] * 0.299 + sourceBitArray[i + 1] * 0.587 +
                                sourceBitArray[i] * 0.114;
                destBitArray[j++] = (byte)colorTemp;
            }

            // 创建ImageInfo对象
            var imageInfo = new ImageInfo
            {
                Width = width,
                Height = height,
                Format = AsfImagePixelFormat.ASVL_PAF_GRAY,
                ImgData = Marshal.AllocHGlobal(destBitArrayLength)
            };

            // 复制灰度字节数组到分配的内存
            Marshal.Copy(destBitArray, 0, imageInfo.ImgData, destBitArrayLength);

            return imageInfo;
        }


        public async Task<Stream> ScaleAsync(Stream image, int dstWidth, int dstHeight)
        {
            image.Reset();
            // Decode the image from the stream
            // 从Stream中加载图片
            SKBitmap original;
            using (var managedStream = new SKManagedStream(image))
            {
                original = SKBitmap.Decode(managedStream);
            }


            // Calculate the scale rate and the new width and height
            var scaleRate = GetWidthAndHeight(original.Width, original.Height, dstWidth, dstHeight);
            var width = (int)(original.Width * scaleRate);
            var height = (int)(original.Height * scaleRate);

            // Adjust the width to be a multiple of 4
            width = width - (width % 4);

            // Create a new SKImageInfo with the new width and height
            var info = new SKImageInfo(width, height);

            // Create a new surface with the SKImageInfo
            var surface = SKSurface.Create(info);

            // Get the canvas from the surface to draw on
            var canvas = surface.Canvas;

            // Clear the canvas with a transparent color
            canvas.Clear(SKColors.Transparent);

            // Set the quality of the canvas
            var paint = new SKPaint
            {
                IsAntialias = true,
                FilterQuality = SKFilterQuality.High
            };

            // Draw the scaled image
            canvas.DrawBitmap(original, new SKRect(0, 0, width, height), paint);

            // Encode the surface into a stream
            var scaledImage = surface.Snapshot();
            var data = scaledImage.Encode(SKEncodedImageFormat.Png, 100);
            var scaledStream = new MemoryStream();
            data.SaveTo(scaledStream);
            scaledStream.Seek(0, SeekOrigin.Begin);

            return await Task.FromResult(scaledStream);
        }
        /// <summary>
        /// 获取图片缩放比例
        /// </summary>
        /// <param name="oldWidth">原图片宽</param>
        /// <param name="oldHeight">原图片高</param>
        /// <param name="newWidth">目标图片宽</param>
        /// <param name="newHeight">目标图片高</param>
        /// <returns></returns>
        private static float GetWidthAndHeight(int oldWidth, int oldHeight, int newWidth, int newHeight)
        {
            //按比例缩放           
            float scaleRate;
            if (oldWidth >= newWidth && oldHeight >= newHeight)
            {
                var widthDis = oldWidth - newWidth;
                var heightDis = oldHeight - newHeight;

                scaleRate = widthDis > heightDis ? newWidth * 1f / oldWidth : newHeight * 1f / oldHeight;
            }
            else if (oldWidth >= newWidth && oldHeight < newHeight)
            {
                scaleRate = newWidth * 1f / oldWidth;
            }
            else if (oldWidth < newWidth && oldHeight >= newHeight)
            {
                scaleRate = newHeight * 1f / oldHeight;
            }
            else
            {
                var widthDis = newWidth - oldWidth;
                var heightDis = newHeight - oldHeight;
                if (widthDis > heightDis)
                    scaleRate = newHeight * 1f / oldHeight;
                else
                    scaleRate = newWidth * 1f / oldWidth;
            }

            return scaleRate;
        }

    }
}
