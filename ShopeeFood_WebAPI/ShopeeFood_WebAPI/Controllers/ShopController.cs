using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFood_WebAPI.BLL.IServices;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopServices _services;
        private readonly IMapper _mapper;

        public ShopController(IShopServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet("{shopId}")]
        public async Task<IActionResult> GetShopInfo(int shopId)
        {
            var info = await _services.GetShopInfo(shopId);
            if (info == null)
            {
                return NotFound(new ApiResponse
                {
                    status = HttpStatusCode.NotFound.ToString(),
                    message = ApiResponseMessage.NOT_FOUND,
                    data = ""
                });
            }
            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK.ToString(),
                message = ApiResponseMessage.SUCCESS,
                data = JsonConvert.SerializeObject(info)
            });
        }

        [HttpGet("menu/{shopId}")]
        public async Task<IActionResult> GetShopMenuInfo(int shopId)
        {
            var menu = await _services.GetShopMenu(shopId);
            if (menu == null)
            {
                return NotFound(new ApiResponse
                {
                    status = HttpStatusCode.NotFound.ToString(),
                    message = ApiResponseMessage.NOT_FOUND,
                    data = ""
                });
            }
            //return Ok(new ApiResponse
            //{
            //    status = HttpStatusCode.OK.ToString(),
            //    message = ApiResponseMessage.SUCCESS,
            //    data = JsonConvert.SerializeObject(menu)
            //});
            return Ok(menu);
        }

    }
}
