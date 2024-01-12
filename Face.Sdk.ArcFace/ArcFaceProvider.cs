using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using Face.Sdk.ArcFace.Implementations;
using Face.Sdk.ArcFace.Processor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Face.Sdk.ArcFace
{
    public class ArcFaceProvider : IFaceProvider
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
        public bool FaceCompare(Image img1, Image img2)
        {

            // 提取人脸特征
            var features0 = arcFace.ExtractFaceFeatureAsync(ImageUtil.GetImageStream(img1)).Result;
            var features1 = arcFace.ExtractFaceFeatureAsync(ImageUtil.GetImageStream(img2)).Result;

            // 人脸比对
            var result = arcFace.CompareFaceFeatureAsync(features0.Data.Single(), features1.Data.Single()).Result;
            return result.Data>0.9f;
        }

        public List<FaceDetectorDto> FaceDetector(Image image)
        {
            var result = arcFace.DetectFaceAsync(ImageUtil.GetImageStream(image)).GetAwaiter().GetResult();
            return result.Data.Faces.Select(r => new FaceDetectorDto
            {
                Score=r.FaceOrient
            }).ToList();
        }
    }
}
