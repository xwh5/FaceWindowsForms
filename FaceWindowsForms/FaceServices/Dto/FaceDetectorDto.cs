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
        public float Score { get; set; }

        public readonly int x;
        public readonly int y;
        public readonly int width;
        public readonly int height;
    }
}
