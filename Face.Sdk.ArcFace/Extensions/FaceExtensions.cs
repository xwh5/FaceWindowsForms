using Face.Sdk.ArcFace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Face.Sdk.ArcFace.Extensions
{
    public static class FaceExtensions
    {
        /// <summary>
        /// 将字节数组转为人脸特征指针
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public static IntPtr ToFaceFeature(this byte[] feature)
        {
            try
            {
                var localFeature = new AsfFaceFeature { Feature = Marshal.AllocHGlobal(feature.Length) };
                Marshal.Copy(feature, 0, localFeature.Feature, feature.Length);
                localFeature.FeatureSize = feature.Length;

                var pLocalFeature = Marshal.AllocHGlobal(Marshal.SizeOf<AsfFaceFeature>());
                Marshal.StructureToPtr(localFeature, pLocalFeature, false);
                return pLocalFeature;
            }
            catch (Exception e)
            {
                throw new InvalidFaceFeatureException(e);
            }
        }

        /// <summary>
        /// 将图片文件转为Stream
        /// </summary>
        /// <param name="image">图片路径</param>
        /// <returns>图片流</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static Stream ToStream(this string image)
        {
            if (!File.Exists(image))
                throw new FileNotFoundException($"{image} doesn't exist.");

            return File.OpenRead(image);
        }

        public static void Reset(this Stream stream)
        {
            if (stream == null)
                return;
            if (stream.Position > 0)
                stream.Seek(0, SeekOrigin.Begin);
        }
    }
}
