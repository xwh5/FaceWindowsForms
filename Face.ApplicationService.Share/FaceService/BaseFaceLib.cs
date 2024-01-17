using Face.ApplicationService.Share;
using Face.ApplicationService.Share.FaceService;
using Face.ApplicationService.Share.FaceService.Dto;
using SkiaSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Face.ApplicationService.Share.FaceService
{
    public abstract class BaseFaceLib<T> : IBaseFaceLib
    {
        public ConcurrentDictionary<string, List<FaceLibItem<T>>> Store { get; set; } = new ConcurrentDictionary<string, List<FaceLibItem<T>>>();

        private IFaceFeature<T> faceFeature;
        public string key;
        public BaseFaceLib(IFaceProvider faceProvide,string key)
        {
            this.key = key;
            this.faceFeature = faceProvide as IFaceFeature<T>;
        }
        public virtual void Add(string name, string imgUrl, T feature)
        {
            if (Store.TryGetValue(name, out var item))
            {
                item.Add(new FaceLibItem<T>
                {
                    ImgUrl = imgUrl,
                    Feature = feature
                });
            }
            else
            {
                Store.TryAdd(name, new List<FaceLibItem<T>> { new FaceLibItem<T>
                {
                    ImgUrl = imgUrl,
                    Feature = feature
                }});
            }
        }

        public virtual void Delete(string name, string imgUrl)
        {
            if (Store.TryGetValue(name, out var item))
            {
                item.RemoveAt(item.FindIndex(r => r.ImgUrl == imgUrl));
            }
        }

        public virtual string Search(Image img)
        {
            var data = faceFeature.GetFeature(img);
            if (Store == null) return null;
            if (data == null)
            {
                return null;
            }
            foreach (var item in Store)
            {
                if (item.Value.Any(r => faceFeature.Compare(data, r.Feature)))
                {
                    return item.Key;
                }
            }
            return null;
        }

        public virtual string Search(SKBitmap img)
        {
            var data = faceFeature.GetFeature(img);
            var faceInfo = new List<FaceInfo<T>>();
            using (var dapper = new DapperHelper())
            {
                faceInfo = dapper.GetList<T>(key).Result.ToList();
            }
            if (faceInfo == null) return null;
            if (data == null)
            {
                return null;
            }
            foreach (var item in faceInfo)
            {
                if (faceFeature.Compare(data, item.Feature))
                {
                    return item.Name;
                }
            }
            return "数据库中为找到此人";
        }

        public void InitFaceLib(string path)
        {

            string[] extensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

            var item = new List<FaceLibItem<T>>();
            foreach (string file in Directory.GetFiles(path))
            {
                if (extensions.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase)))
                {
                    using (var img = SKBitmap.Decode(file))
                    {
                        using (var dapper = new DapperHelper())
                        {
                            if (dapper.IsExist(file,key).Result)
                            {
                                continue;
                            }
                            var f = faceFeature.GetFeature(img);
                            if (f != null)
                            {
                                dapper.Add(file, f, key).GetAwaiter().GetResult();
                            }
           
                        }
                        
                    }

                    //using (var img = Image.FromFile(file))
                    //{
                    //    var f = faceFeature.GetFeature(img);
                    //    if (f != null)
                    //    {
                    //        item.Add(new FaceLibItem<T>
                    //        {
                    //            ImgUrl = file,
                    //            Feature = f
                    //        });
                    //    }
                    //}
                }
            }
        }
    }
}
