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
        private Net faceRecognizer;
        private Net faceRecognizer2;
        public override void Dispose()
        {
            faceRecognizer?.Dispose();
            faceCascade?.Dispose();
        }
        public OpencvSharpProvider()
        {
            faceCascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            //faceRecognizer = EigenFaceRecognizer.Create();
            faceRecognizer = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7");
            faceRecognizer2 = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7");
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

            // 检测第一张图像中的人脸
            Rect[] faces1 = faceCascade.DetectMultiScale(image1, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(90, 90));
            Mat face1 = new Mat(image1, faces1[0]); // 裁剪出人脸区域

            // 检测第二张图像中的人脸
            Rect[] faces2 = faceCascade.DetectMultiScale(image2, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(90, 90));
            Mat face2 = new Mat(image2, faces2[0]); // 裁剪出人脸区域

            //提取第一张图像的人脸特征
            using (var faceRecognizer = CvDnn.ReadNetFromOnnx("face_detection_yunet_2023mar.onnx"))
            {
                using (var faceRecognizer2 = CvDnn.ReadNetFromOnnx("face_detection_yunet_2023mar.onnx"))
                {
                    Mat blob1 = CvDnn.BlobFromImage(face1, 1.0 / 255, new OpenCvSharp.Size(320, 320), new Scalar(0, 0, 0), true, false);
                    faceRecognizer.SetInput(blob1);
                    Mat faceFeature1 = faceRecognizer.Forward();

                    Mat faceBlob2 = CvDnn.BlobFromImage(face2, 1.0 / 255, new OpenCvSharp.Size(320, 320), new Scalar(0, 0, 0), true, false);
                    faceRecognizer2.SetInput(faceBlob2);
                    Mat faceFeature2 = faceRecognizer2.Forward();
                    double similarity = Cv2.Norm(faceFeature1, faceFeature2, NormTypes.L2);

                    return similarity < 18;
                }

            }

            //// 提取第一张图像的人脸特征
            //using (var faceRecognizer = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7"))
            //{
            //    using (var faceRecognizer2 = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7"))
            //    {
            //        Mat blob1 = CvDnn.BlobFromImage(face1, 1.0 / 255, new OpenCvSharp.Size(96, 96), new Scalar(0, 0, 0), true, false);
            //        faceRecognizer.SetInput(blob1);
            //        Mat faceFeature1 = faceRecognizer.Forward();

            //        Mat faceBlob2 = CvDnn.BlobFromImage(face2, 1.0 / 255, new OpenCvSharp.Size(96, 96), new Scalar(0, 0, 0), true, false);
            //        faceRecognizer2.SetInput(faceBlob2);
            //        Mat faceFeature2 = faceRecognizer2.Forward();
            //        double similarity = Cv2.Norm(faceFeature1, faceFeature2, NormTypes.L2);

            //        return similarity <1.09;
            //    }

            //}


        }


        public bool Compare(Mat data, Mat dest)
        {
            using (var faceRecognizer = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7"))
            {
                using (var faceRecognizer2 = CvDnn.ReadNetFromTorch("openface.nn4.small2.v1.t7"))
                {
                    Mat blob1 = CvDnn.BlobFromImage(data, 1.0 / 255, new OpenCvSharp.Size(96, 96), new Scalar(0, 0, 0), true, false);
                    faceRecognizer.SetInput(blob1);
                    Mat faceFeature1 = faceRecognizer.Forward();

                    Mat faceBlob2 = CvDnn.BlobFromImage(dest, 1.0 / 255, new OpenCvSharp.Size(96, 96), new Scalar(0, 0, 0), true, false);
                    faceRecognizer2.SetInput(faceBlob2);
                    Mat faceFeature2 = faceRecognizer2.Forward();
                    double similarity = Cv2.Norm(faceFeature1, faceFeature2, NormTypes.L2);

                    return similarity < 1.09;
                }

            }
        }

        public Mat GetFeature(System.Drawing.Image img1)
        {
            using (Mat image1 = Mat.FromStream(ImageUtil.GetImageStream(img1), ImreadModes.Color))
            {
                // 检测第一张图像中的人脸
                Rect[] faces1 = faceCascade.DetectMultiScale(image1, 1.1, 6, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(90, 90));
                if (faces1.Length > 0)
                {
                    Mat face1 = new Mat(image1, faces1[0]); // 裁剪出人脸区域
                    return face1;
                }
                else
                {
                    return null;
                }

            };
        }
    }
}
