using Face.ApplicationService.FaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWindowsForms
{
    public static class FaceGlobal
    {
        /// <summary>
        /// 全局key
        /// </summary>
        public static string Key { get; set; } = "ArcFace";

        /// <summary>
        /// 人脸识别服务
        /// </summary>
        public static FaceService CurrentFaceService;

        public static string[] ComboBox1Data = new string[] {
            "ArcFace","ViewFaceCode","FaceRecognitionDotNet",
        };

        public static string FaceLibPath = @"D:\Users\xwh73\Downloads\FaceLib";
    }
}
