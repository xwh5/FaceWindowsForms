
using Face.Sdk.ArcFace;

namespace Face.Sdk.ArcFace.Implementations
{
    public partial class ArcFaceHandler : IArcFace
    {
        private IImageProcessor _processor;
        private readonly ArcFaceOptions _options;

        //public ArcFace(IImageProcessor processor, IOptionsMonitor<ArcFaceOptions> options) : this(processor,
        //    options.CurrentValue)
        //{
        //}

        public ArcFaceHandler(IImageProcessor processor, ArcFaceOptions options,bool isAction)
        {
            _processor = processor;
            _options = options;
            if (isAction)
            {

                OnlineActiveAsync().Wait();
            }
        }
    }
}

