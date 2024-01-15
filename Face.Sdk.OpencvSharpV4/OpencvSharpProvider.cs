using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Face.Sdk.OpencvSharpV4
{
    public class OpencvSharpProvider : FaceProvider, IFaceFeature<Mat>
    {
        private CascadeClassifier faceCascade;
        public override void Dispose()
        {
            //faceRecognizer?.Dispose();
            faceCascade?.Dispose();
        }
        public OpencvSharpProvider()
        {
            faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            //faceRecognizer = EigenFaceRecognizer.Create();
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

            Mat image1 = Mat.FromStream(ImageUtil.GetImageStream(img1), ImreadModes.Color);
            Mat image2 = Mat.FromStream(ImageUtil.GetImageStream(img2), ImreadModes.Color);
            //提取第一张图像的人脸特征
            //using (var faceRecognizer = CvDnn.ReadNetFromOnnx("arcfaceresnet100-11-int8.onnx"))
            //{
            //    using (var faceRecognizer2 = CvDnn.ReadNetFromOnnx("arcfaceresnet100-11-int8.onnx"))
            //    {
            //        Mat blob1 = CvDnn.BlobFromImage(image1, 1.0 / 256, new OpenCvSharp.Size(112, 112), swapRB: true);
            //        faceRecognizer.SetInput(blob1);
            //        Mat faceFeature1 = faceRecognizer.Forward();

            //        Mat faceBlob2 = CvDnn.BlobFromImage(image2, 1.0 / 256, new OpenCvSharp.Size(112, 112), swapRB: true);
            //        faceRecognizer2.SetInput(faceBlob2);
            //        Mat faceFeature2 = faceRecognizer2.Forward();
            //        //double similarity = Cv2.Norm(faceFeature1, faceFeature2, NormTypes.L2);

            //        //归一化特征向量
            //        Cv2.Normalize(faceFeature1, faceFeature1, 1, 0, NormTypes.L2);
            //        Cv2.Normalize(faceFeature2, faceFeature2, 1, 0, NormTypes.L2);

            //        // 计算余弦距离
            //        double similarity = faceFeature1.Dot(faceFeature2);
            //        return similarity > 0.8;
            //    }

            //}

            // 提取第一张图像的人脸特征
            using (var faceRecognizer = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7"))
            {
                using (var faceRecognizer2 = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7"))
                {
                    Mat blob1 = CvDnn.BlobFromImage(image1, 1.0 / 256, new OpenCvSharp.Size(96, 96), new Scalar(0, 0, 0), true, false);
                    faceRecognizer.SetInput(blob1);
                    Mat faceFeature1 = faceRecognizer.Forward();

                    Mat faceBlob2 = CvDnn.BlobFromImage(image2, 1.0 / 256, new OpenCvSharp.Size(96, 96), new Scalar(0, 0, 0), true, false);
                    faceRecognizer2.SetInput(faceBlob2);
                    Mat faceFeature2 = faceRecognizer2.Forward();
                    //double similarity = Cv2.Norm(faceFeature1, faceFeature2, NormTypes.L2);
                    //归一化特征向量
            Cv2.Normalize(faceFeature1, faceFeature1, 1, 0, NormTypes.L2);
                    Cv2.Normalize(faceFeature2, faceFeature2, 1, 0, NormTypes.L2);
                    //计算余弦距离
                    double similarity = faceFeature1.Dot(faceFeature2);
                    return similarity < 1.19;
                }
            }
        }


        public bool Compare(Mat data, Mat dest)
        {
            //提取第一张图像的人脸特征
            using (var faceRecognizer = CvDnn.ReadNetFromOnnx("arcfaceresnet100-11-int8.onnx"))
            {
                using (var faceRecognizer2 = CvDnn.ReadNetFromOnnx("arcfaceresnet100-11-int8.onnx"))
                {
                    Mat blob1 = CvDnn.BlobFromImage(data, 1.0 / 256, new OpenCvSharp.Size(112, 112), new Scalar(0, 0, 0), false, false);
                    faceRecognizer.SetInput(blob1);
                    Mat faceFeature1 = faceRecognizer.Forward();

                    Mat faceBlob2 = CvDnn.BlobFromImage(dest, 1.0 / 256, new OpenCvSharp.Size(112, 112), new Scalar(0, 0, 0), false, false);
                    faceRecognizer2.SetInput(faceBlob2);
                    Mat faceFeature2 = faceRecognizer2.Forward();
                    //double similarity = Cv2.Norm(faceFeature1, faceFeature2, NormTypes.L2);

                    //return similarity < 18;
                    //归一化特征向量
                    Cv2.Normalize(faceFeature1, faceFeature1, 1, 0, NormTypes.L2);
                    Cv2.Normalize(faceFeature2, faceFeature2, 1, 0, NormTypes.L2);

                    // 计算余弦距离
                    double similarity = faceFeature1.Dot(faceFeature2);
                    return similarity > 0.9;
                }

            }
        }

        public Mat GetFeature(System.Drawing.Image img1)
        {
            Mat image1 = Mat.FromStream(ImageUtil.GetImageStream(img1), ImreadModes.Color);
                // 检测第一张图像中的人脸
                Rect[] faces1 = faceCascade.DetectMultiScale(image1, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(90, 90));
            if (faces1.Length > 0)
            {
                //Mat face1 = new Mat(image1, faces1[0]); // 裁剪出人脸区域
                return image1;
            }
            else
            {
                image1.Dispose();
                return null;
            }
        }
    }
}
