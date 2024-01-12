using Face.Sdk.ArcFace.Extensions;
using Face.Sdk.ArcFace.Models;
using Face.Sdk.ArcFace.Utils;
using System.IO;
using System.Threading.Tasks;

namespace Face.Sdk.ArcFace.Implementations
{
    /// <summary>
    /// 人脸属性 3D角度/年龄/性别
    /// </summary>
    public partial class ArcFaceHandler
    {
        public async Task<OperationResult<Face3DAngle>> GetFace3DAngleAsync(string image)
        {
            using (var img = image.ToStream()) {
                return await GetFace3DAngleAsync(img);
            }

        }

        public async Task<OperationResult<Face3DAngle>> GetFace3DAngleAsync(Stream image) =>
            await ProcessImageAsync<AsfFace3DAngle, Face3DAngle>(image, FaceHelper.GetFace3DAngleAsync);

        public async Task<OperationResult<AgeInfo>> GetAgeAsync(string image)
        {
            using (var img = image.ToStream()) {
                return await GetAgeAsync(img);
            }

        }

        public async Task<OperationResult<AgeInfo>> GetAgeAsync(Stream image) =>
            await ProcessImageAsync<AsfAgeInfo, AgeInfo>(image, FaceHelper.GetAgeAsync);

        public async Task<OperationResult<GenderInfo>> GetGenderAsync(string image)
        {
            using (var img = image.ToStream()) {
                return await GetGenderAsync(img);
            }
        }

        public async Task<OperationResult<GenderInfo>> GetGenderAsync(Stream image) =>
            await ProcessImageAsync<AsfGenderInfo, GenderInfo>(image, FaceHelper.GetGenderAsync);
    }
}