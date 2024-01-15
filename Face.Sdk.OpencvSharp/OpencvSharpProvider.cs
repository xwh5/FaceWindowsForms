using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using OpenCvSharp;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Face.Sdk.OpencvSharp
{
    public class OpencvSharpProvider : FaceProvider, IFaceFeature<Mat>
    {
        private CascadeClassifier faceCascade;
        private EigenFaceRecognizer faceRecognizer;
        public OpencvSharpProvider()
        {
            faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            faceRecognizer = EigenFaceRecognizer.Create();
        }
        public override List<FaceDetectorDto> FaceDetector(System.Drawing.Image image)
        {
            Mat image1 = Mat.FromStream(ImageUtil.GetImageStream(image), ImreadModes.Grayscale);
            Rect[] faces1 = faceCascade.DetectMultiScale(image1, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));
            return faces1.Select(r => new FaceDetectorDto
            {
                Score = 1,
                x = r.X,
                y = r.Y,
                width = r.Width,
                height = r.Height,
            }).ToList();
        }

        public override bool FaceCompare(System.Drawing.Image img1, System.Drawing.Image img2)
        {
            // 加载两张要比对的人脸图像

            Mat image1 = Mat.FromStream(ImageUtil.GetImageStream(img1), ImreadModes.Grayscale);
            Mat image2 = Mat.FromStream(ImageUtil.GetImageStream(img2), ImreadModes.Grayscale);

            // 检测第一张图像中的人脸
            Rect[] faces1 = faceCascade.DetectMultiScale(image1, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));
            Mat face1 = new Mat(image1, faces1[0]); // 裁剪出人脸区域

            // 检测第二张图像中的人脸
            Rect[] faces2 = faceCascade.DetectMultiScale(image2, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));
            Mat face2 = new Mat(image2, faces2[0]); // 裁剪出人脸区域

            // 训练人脸识别器
            faceRecognizer.Train(new Mat[] { face1 }, new int[] { 0 });

            // 使用人脸识别器对第二张图像的人脸进行预测
            var predictedResult = faceRecognizer.Predict(face2);

            return false;
        }

        public bool Compare(Mat data, Mat dest)
        {

            // 训练人脸识别器
            faceRecognizer.Train(new Mat[] { data }, new int[] { 0 });

            // 使用人脸识别器对第二张图像的人脸进行预测
            var predictedResult = faceRecognizer.Predict(dest);
            return true;
        }

        public Mat GetFeature(System.Drawing.Image img1)
        {
            Mat image1 = Mat.FromStream(ImageUtil.GetImageStream(img1), ImreadModes.Grayscale);
            // 检测第一张图像中的人脸
            Rect[] faces1 = faceCascade.DetectMultiScale(image1, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));
            Mat face1 = new Mat(image1, faces1[0]); // 裁剪出人脸区域
            return face1;
        }
    }
}
