using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.Infrastructure.Common;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityServies _services;
        private readonly IMapper _mapper;
        
        public CityController(ICityServies services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _services.GetAll();
            if (!items.Any())
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
                data = JsonConvert.SerializeObject(items)
            });
        }
    }
}
