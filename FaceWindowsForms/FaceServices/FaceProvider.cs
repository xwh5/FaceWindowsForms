using FaceWindowsForms.FaceServices.Dto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWindowsForms.FaceServices
{
    public class FaceProvider 
    {
        public virtual List<FaceDetectorDto> FaceDetector(Bitmap image)
        {
            throw new NotImplementedException();
        }
        public virtual bool FaceCompare(Bitmap img1, Bitmap img2)
        {
            throw new NotImplementedException();
        }
    }
}
