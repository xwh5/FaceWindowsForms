using System;
using System.Collections.Generic;
using System.Text;

namespace Face.ApplicationService.Share.FaceService.Dto
{
    public class FaceLibItem<T>
    {
        public string ImgUrl { get; set; }

        public T Feature { get; set; }
    }
}
