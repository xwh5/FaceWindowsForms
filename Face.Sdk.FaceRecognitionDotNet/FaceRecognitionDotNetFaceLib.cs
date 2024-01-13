using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using FaceRecognitionDotNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Face.Sdk.FaceRecognitionDotNet
{
    public class FaceRecognitionDotNetFaceLib : BaseFaceLib<FaceEncoding>
    {
        public FaceRecognitionDotNetFaceLib(IFaceProvider faceFeature) : base(faceFeature)
        {
        }
    }
}
