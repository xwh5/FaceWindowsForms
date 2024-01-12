using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Face.Sdk.ArcFace.Models;
using Face.Sdk.ArcFace.Utils;

namespace Face.Sdk.ArcFace.Implementations
{
    /// <summary>
    /// SDK信息 激活信息/版本信息
    /// </summary>
    public partial class ArcFaceHandler
    {
        public async Task<OperationResult<ActiveFileInfo>> GetActiveFileInfoAsync() =>
            await Task.Run(() =>
            {
                var pointer = IntPtr.Zero;
                try
                {
                    pointer = Marshal.AllocHGlobal(Marshal.SizeOf<AsfActiveFileInfo>());
                    var code = AsfHelper.ASFGetActiveFileInfo(pointer);
                    if (code != 0)
                        return new OperationResult<ActiveFileInfo>(code);

                    var info = Marshal.PtrToStructure<AsfActiveFileInfo>(pointer);
                    return new OperationResult<ActiveFileInfo>(info.Cast());
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                        Marshal.FreeHGlobal(pointer);
                }
            });

        public async Task<VersionInfo> GetSdkVersionAsync() =>
            await Task.Run(() =>
            {
                var pointer = IntPtr.Zero;
                try
                {
                    pointer = Marshal.AllocHGlobal(Marshal.SizeOf<AsfVersionInfo>());
                    AsfHelper.ASFGetVersion(pointer);
                    var version = Marshal.PtrToStructure<AsfVersionInfo>(pointer);
                    return version.Cast();
                }
                finally
                {
                    Marshal.FreeHGlobal(pointer);
                }
            });
    }
}