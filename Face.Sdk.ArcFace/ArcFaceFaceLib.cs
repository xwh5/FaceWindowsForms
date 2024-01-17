using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Face.Sdk.ArcFace
{
    public class ArcFaceFaceLib : BaseFaceLib<byte[]>
    {
        public ArcFaceFaceLib(IFaceProvider faceFeature,string key) : base(faceFeature,key)
        {
        }
    }
}
