using System;
using System.Collections.Generic;
using System.Text;

namespace Face.ApplicationService.Share.FaceService.Dto
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
