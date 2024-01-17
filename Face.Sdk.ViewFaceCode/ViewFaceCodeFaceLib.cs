using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Face.Sdk.ViewFaceCode
{
    public class ViewFaceCodeFaceLib: BaseFaceLib<float[]>
    {
        public ViewFaceCodeFaceLib(IFaceProvider faceProvider,string key) : base(faceProvider,key) { }
    }
}
