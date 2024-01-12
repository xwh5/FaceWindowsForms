using Face.Sdk.ArcFace.Extensions;
using Face.Sdk.ArcFace.Models;
using Face.Sdk.ArcFace.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Face.Sdk.ArcFace.Implementations
{
    /// <summary>
    /// 工具方法
    /// </summary>
    public partial class ArcFaceHandler
    {

        private (ConcurrentQueue<IntPtr> Engines, int EnginesCount, EventWaitHandle EnginesWaitHandle) GetEngineStuff(DetectionModeEnum mode) {
            switch (mode)
            {
                case DetectionModeEnum.Image:
                    return (_imageEngines, ASF_IMAGE_ENGINES_COUNT,
                    _imageWaitHandle);
                case DetectionModeEnum.Video:
                    return (_videoEngines, ASF_VIDEO_ENGINES_COUNT,
                    _videoWaitHandle);
                case DetectionModeEnum.RGB:
                    return (_rgbEngines, ASF_RGB_ENGINES_COUNT, _rgbWaitHandle);
                case DetectionModeEnum.IR:
                    return (_irEngines, ASF_IR_ENGINES_COUNT, _irWaitHandle);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "invalid detection mode");
    
            }
        }
        private async Task<OperationResult<TK>> ProcessImageAsync<T, TK>(Stream image,
            Func<IntPtr, ImageInfo, Task<OperationResult<T>>> process, DetectionModeEnum mode = DetectionModeEnum.Image)
        {
            var engine = IntPtr.Zero;
            try
            {
                using (var imageInfo = await _processor.VerifyAsync(image))
                {
                    engine = GetEngine(mode);
                    var result = await process(engine, imageInfo);
                    return (await process(engine, imageInfo)).Cast<TK>();
                }

            }
            finally
            {
                RecycleEngine(engine, mode);
            }
        }

        private async Task<OperationResult<TK>> ProcessImageAsync<T, TK>(Stream image,
            Func<IntPtr, (ImageInfo, ImageInfo), Task<OperationResult<T>>> process,
            DetectionModeEnum mode = DetectionModeEnum.Image)
        {
            var engine = IntPtr.Zero;
            try
            {
                engine = GetEngine(mode);
                var (rgb, ir) = await _processor.VerifyIrAsync(image);
                using (rgb)
                {
                     using (ir)
                    {
                        return (await process(engine, (rgb, ir))).Cast<TK>();
                    }
                }
            }
            finally
            {
                RecycleEngine(engine, mode);
            }
        }


        private async Task<(IEnumerable<FaceData> Faces, IEnumerable<NoFaceImageException> Exceptions)>
            ExtractFaceFeaturesAsync<T>(
                params T[] images)
        {
            var faces = new List<FaceData>();
            var exceptions = new List<NoFaceImageException>();
            if (images == null || !images.Any())
                return (faces, exceptions);

            var engine = IntPtr.Zero;
            try
            {
                engine = GetEngine(DetectionModeEnum.Image);
                foreach (var image in images)
                {
                    if (image is string image0)
                    {
                        using (var img = image0.ToStream()) {
                            using (var imageInfo = await _processor.VerifyAsync(img)){

                                var feature = await FaceHelper.ExtractSingleFeatureAsync(engine, imageInfo);
                                if (feature.Code != 0)
                                {
                                    exceptions.Add(
                                        new NoFaceImageException(feature.Code, image0));
                                    continue;
                                }

                                faces.Add(new FaceData(
                                    Path.GetFileNameWithoutExtension(image0), feature.Data));
                            } 
                        }
                        
                    }
                    else
                    {
                        var img = image as Stream;
                        using (var imageInfo = await _processor.VerifyAsync(image as Stream)) {

                            var feature = await FaceHelper.ExtractSingleFeatureAsync(engine, imageInfo);
                            if (feature.Code != 0)
                            {
                                exceptions.Add(
                                    new NoFaceImageException(feature.Code));
                                continue;
                            }

                            faces.Add(new FaceData(null, feature.Data));
                        }
                        
                    }
                }

                return (faces, exceptions);
            }
            finally
            {
                RecycleEngine(engine, DetectionModeEnum.Image);
            }
        }
    }
}