using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWindowsForms.FaceServices.Dto
{
    public class FaceDetectorDto
    {
        /// <summary>
        /// 是否人脸
        /// </summary>
        public double Score { get; set; }

        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
