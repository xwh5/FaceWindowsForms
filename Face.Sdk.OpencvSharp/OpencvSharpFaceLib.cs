using Face.ApplicationService.Share.FaceService;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Face.Sdk.OpencvSharp
{
    public class OpencvSharpFaceLib : BaseFaceLib<Rect[]>
    {
        public OpencvSharpFaceLib(IFaceProvider faceProvider) : base(faceProvider) { }
    }
}
