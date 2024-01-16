
using Face.ApplicationService.FaceService;
using FaceAspnetcore.Input;
using Microsoft.AspNetCore.Mvc;

namespace FaceAspnetcore
{
    [ApiController]
    [Route("api/Init")]
    public class InitController : ControllerBase
    {
        [HttpPost]
        public async Task Init( InitInput input)
        {
            var arcsoft = new FaceService(input.Type, null);
        }
    }
}
