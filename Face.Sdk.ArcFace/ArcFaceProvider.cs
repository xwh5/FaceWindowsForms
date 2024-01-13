using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using Face.Sdk.ArcFace.Implementations;
using Face.Sdk.ArcFace.Models;
using Face.Sdk.ArcFace.Processor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Face.Sdk.ArcFace
{
    public class ArcFaceProvider : FaceProvider, IFaceFeature<byte[]>
    {
        private IArcFace arcFace;
        public ArcFaceProvider() {
            Init();
        }
        private void Init()
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
            });
        }
        public override bool FaceCompare(Image img1, Image img2)
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

        public  override List<FaceDetectorDto> FaceDetector(Image image)
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

        public override void Dispose()
        {
            arcFace.Dispose();
        }


        public byte[] GetFeature(Image img1)
        {
            using (var img= ImageUtil.GetImageStream(img1))
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
