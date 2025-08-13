using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood_WebAPI.BLL.IServices;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/pagesettings")]
    [ApiController]
    public class PageSettingController : ControllerBase
    {
        private readonly IPageServices pageServices;

        public PageSettingController(IPageServices pageServices)
        {
            this.pageServices = pageServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var content = await pageServices.GetPages();
            if (content == null)
            {
                return Ok(new ApiResponse
                {
                    status = HttpStatusCode.NotFound.ToString(),
                    message = ApiResponseMessage.NOT_FOUND,
                    data = JsonConvert.SerializeObject(new ApiModelResponse(false, ApiResponseMessage.NOT_FOUND))
                });
            }
            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK.ToString(),
                message = ApiResponseMessage.SUCCESS,
                data = JsonConvert.SerializeObject(content)
            });
        }
    }
}
