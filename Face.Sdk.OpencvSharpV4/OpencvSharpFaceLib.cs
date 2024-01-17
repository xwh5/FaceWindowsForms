using Face.ApplicationService.Share.FaceService;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Face.Sdk.OpencvSharpV4
{
    public class OpencvSharpFaceLib : BaseFaceLib<Mat>
    {
        public OpencvSharpFaceLib(IFaceProvider faceProvider,string key) : base(faceProvider,key) { }
    }
}
