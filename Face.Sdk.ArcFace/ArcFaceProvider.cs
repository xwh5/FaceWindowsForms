using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using Face.Sdk.ArcFace.Implementations;
using Face.Sdk.ArcFace.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace Face.Sdk.ArcFace
{
    public class ArcFaceProvider : FaceProvider, IFaceFeature<byte[]>
    {
        private IArcFace arcFace;
        public ArcFaceProvider(bool isAction) {
            Init(isAction);
        }
        private void Init(bool isAction)
        {
            arcFace = new ArcFaceHandler(new ImageProcessor(), new ArcFaceOptions
            {
                AppId = "2k5TiH7B161bjt8zMr9TShScds6vMDYwCudEVtR6QvF5",
                SdkKeys = new SdkKeys
                {
                    Winx64 = "GMGRHcDPLqKYYPkzRGiUdu35hHezCuwTqv8gBmMYUpEp",
                    Linux64 = "GMGRHcDPLqKYYPkzRGiUdu35ZB5sxqtszKbixYat9jTU"
                },
                MinSimilarity = 0.6f
            }, isAction);
        }
        public override bool FaceCompare(System.Drawing.Image img1, System.Drawing.Image img2)
        {
            using (var i1 = ImageUtil.GetImageStream(img1))
            {
                using (var i2 = ImageUtil.GetImageStream(img2))
                {
                    // 提取人脸特征
                    var features0 = arcFace.ExtractFaceFeatureAsync(i1).Result;
                    var features1 = arcFace.ExtractFaceFeatureAsync(i2).Result;

                    // 人脸比对
                    var result = arcFace.CompareFaceFeatureAsync(features0.Data.Single(), features1.Data.Single()).Result;
                    return result.Data > 0.9f;
                }
            }

        }

        public override bool FaceCompare(SKBitmap img1, SKBitmap img2)
        {
            using (var i1 = ImageUtil.GetImageStream(img1))
            {
                using (var i2 = ImageUtil.GetImageStream(img2))
                {
                    // 提取人脸特征
                    var features0 = arcFace.ExtractFaceFeatureAsync(i1).Result;
                    var features1 = arcFace.ExtractFaceFeatureAsync(i2).Result;

                    // 人脸比对
                    var result = arcFace.CompareFaceFeatureAsync(features0.Data.Single(), features1.Data.Single()).Result;
                    return result.Data > 0.9f;
                }
            }
        }

        public  override List<FaceDetectorDto> FaceDetector(System.Drawing.Image image)
        {
            using (var img= ImageUtil.GetImageStream(image))
            {
                var result = arcFace.DetectFaceAsync(img).GetAwaiter().GetResult();
                return result.Data.Faces.Select(r => new FaceDetectorDto
                {
                    Score = r.FaceOrient,
                    height = r.FaceRect.Bottom,
                    width = r.FaceRect.Left,
                    x = r.FaceRect.Right-300,
                    y = r.FaceRect.Top,
                }).ToList();
            }
        }

        public override List<FaceDetectorDto> FaceDetector(SKBitmap skBitmap)
        {
            using (var img = ImageUtil.GetImageStream(skBitmap))
            {
                var result = arcFace.DetectFaceAsync(img).GetAwaiter().GetResult();
                return result.Data.Faces.Select(r => new FaceDetectorDto
                {
                    Score = r.FaceOrient,
                    height = r.FaceRect.Bottom,
                    width = r.FaceRect.Left,
                    x = r.FaceRect.Right - 300,
                    y = r.FaceRect.Top,
                }).ToList();
            }
        }

        public override void Dispose()
        {
            arcFace.Dispose();
        }


        public byte[] GetFeature(System.Drawing.Image img1)
        {
            using (var img= ImageUtil.GetImageStream(img1))
            {
                var result = arcFace.ExtractFaceFeatureAsync(img)?.Result;
                return result?.Data?.FirstOrDefault();
            }
        }

        public byte[] GetFeature(SKBitmap img1)
        {
            using (var img = ImageUtil.GetImageStream(img1))
            {
                var result = arcFace.ExtractFaceFeatureAsync(img)?.Result;
                return result?.Data?.FirstOrDefault();
            }
        }

        public bool Compare(byte[] source, byte[] dest)
        {
            var result = arcFace.CompareFaceFeatureAsync(source, dest).Result;
            return result.Data > 0.9f;
        }
    }
}
