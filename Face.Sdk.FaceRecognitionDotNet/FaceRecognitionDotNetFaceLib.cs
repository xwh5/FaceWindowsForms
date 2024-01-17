using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;

namespace Face.Sdk.FaceRecognitionDotNet
{
    public class FaceRecognitionDotNetFaceLib : BaseFaceLib<byte[]>
    {
        public FaceRecognitionDotNetFaceLib(IFaceProvider faceFeature, string key) : base(faceFeature,key)
        {
        }
    }
}
