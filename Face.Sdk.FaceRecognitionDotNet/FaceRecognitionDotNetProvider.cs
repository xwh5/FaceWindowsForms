using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using FaceRecognitionDotNet;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Image = FaceRecognitionDotNet.Image;


namespace Face.Sdk.FaceRecognitionDotNet
{
    public class FaceRecognitionDotNetProvider : FaceProvider, IFaceFeature<byte[]>
    {
        private FaceRecognition _FaceRecognition;
        public FaceRecognitionDotNetProvider()
        {
            _FaceRecognition = FaceRecognition.Create(AppDomain.CurrentDomain.BaseDirectory + "/Models");
        }
        public override List<FaceDetectorDto> FaceDetector(System.Drawing.Image image)
        {
            var result = _FaceRecognition.FaceLocations(FaceRecognition.LoadImage(new Bitmap(image)));
            return result.Select(r => new FaceDetectorDto
            {
                Score = r.Confidence,
                x = r.Left,
                y = r.Top,
                height = r.Right,
                width = r.Bottom
            }).ToList();
        }

        public override List<FaceDetectorDto> FaceDetector(SKBitmap skBitmap)
        {
            using (var cc = GetImage(skBitmap))
            {
                var result = _FaceRecognition.FaceLocations(cc);

                return result.Select(r => new FaceDetectorDto
                {
                    Score = r.Confidence,
                    x = r.Left,
                    y = r.Top,
                    height = r.Right,
                    width = r.Bottom
                }).ToList();
            }

        }

        private Image GetImage(SKBitmap bitmap)
        {
            // 获取图片的宽度和高度
            int width = bitmap.Width;
            int height = bitmap.Height;
            // 计算stride（一行像素的字节数）
            int stride = width * 3; // 因为RGBA格式每个像素4个字节

            // 将SkiaSharp的SKBitmap转换为字节数组
            byte[] imageData = new byte[height * stride];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var color = bitmap.GetPixel(x, y);
                    var index = (y * bitmap.Width + x) * 3;
                    imageData[index] = color.Red;
                    imageData[index + 1] = color.Green;
                    imageData[index + 2] = color.Blue;
                }
            }
            return FaceRecognition.LoadImage(imageData, height, width, stride, Mode.Rgb);
        }

        public override bool FaceCompare(SKBitmap img1, SKBitmap img2)
        {
            Image cImg1 = null;
            Image cImg2 = null;
            FaceEncoding c1 = null;
            FaceEncoding c2 = null;
            try
            {
                cImg1 = GetImage(img1);
                cImg2 = GetImage(img2);
                c1 = _FaceRecognition.FaceEncodings(cImg1).First();
                c2 = _FaceRecognition.FaceEncodings(cImg2).First();
                return FaceRecognition.CompareFace(c1, c2, 0.5);
            }
            finally
            {
                cImg1.Dispose();
                cImg2.Dispose();
                c1.Dispose();
                c2.Dispose();
            }
        }

        public override bool FaceCompare(System.Drawing.Image img1, System.Drawing.Image img2)
        {
            Image cImg1 = null;
            Image cImg2 = null;
            FaceEncoding c1 = null;
            FaceEncoding c2 = null;
            try
            {

                cImg1 = FaceRecognition.LoadImage(new Bitmap(img1));
                cImg2 = FaceRecognition.LoadImage(new Bitmap(img2));

                c1 = _FaceRecognition.FaceEncodings(cImg1).First();
                c2 = _FaceRecognition.FaceEncodings(cImg2).First();
                return FaceRecognition.CompareFace(c1, c2, 0.5);
            }
            finally
            {
                cImg1.Dispose();
                cImg2.Dispose();
                c1.Dispose();
                c2.Dispose();
            }
        }


        public override void Dispose()
        {
            _FaceRecognition.Dispose();
        }



        public bool Compare(byte[] data, byte[] dest)
        {
            var f1 = FaceRecognition.LoadFaceEncoding(data.ByteToArray());
            var f2 = FaceRecognition.LoadFaceEncoding(dest.ByteToArray());
            return FaceRecognition.CompareFace(f1, f2, 0.5);
        }

        public byte[] GetFeature(SKBitmap img1)
        {
            using (var cImg1 = GetImage(img1))
            {
                var c1 = _FaceRecognition.FaceEncodings(cImg1).First();
                return c1.GetRawEncoding().ArrayToByte();
            }
        }

        public byte[] GetFeature(System.Drawing.Image img1)
        {
            throw new NotImplementedException();
        }
    }
}
