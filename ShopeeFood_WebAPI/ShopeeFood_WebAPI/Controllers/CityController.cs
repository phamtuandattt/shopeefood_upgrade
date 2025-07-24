using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.Infrastructure.Common;
using ShopeeFood_WebAPI.RequestModels.CityRequestDtos;

namespace ShopeeFood_WebAPI.Controllers
{
    [Route("api/cities")]
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

        // Get business field in the city
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!await _services.CityExisted(id))
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
                data = JsonConvert.SerializeObject(await _services.GetAllBusiness(id))
            });
        }

        // Get shop field in the city
        [HttpPost("shops")]
        public async Task<IActionResult> GetShopInTheCity([FromBody] CityFieldShopRequestDto modelRequest)
        {
            var items = await _services.GetShopInTheCity(modelRequest.CityID, modelRequest.FieldID, modelRequest.PageNumber, modelRequest.PageSize);
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

        // Get shop and busines in the city
        [HttpPost("business-shops")]
        public async Task<IActionResult> GetShopCityBusinesses([FromBody] CityFieldShopRequestDto modelRequest)
        {
            var items = await _services.GetShopCityBusiness(modelRequest.CityID, modelRequest.FieldID, modelRequest.PageNumber, modelRequest.PageSize);
            if (items == null)
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCityRequestDto cityRequestDto)
        {
            try
            {
                var item = _mapper.Map<CityDto>(cityRequestDto);
                await _services.AddAsync(item);
            }
            catch (DbUpdateException)
            {

            }
            return Ok(new ApiResponse
            {
                status = HttpStatusCode.NoContent + "",
                message = ApiResponseMessage.SUCCESS,
                data = ""
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCityRequestDto cityRequestDto)
        {
            var item = _mapper.Map<CityDto>(cityRequestDto);
            var success = await _services.UpdateAsync(id, item);
            if (!success)
            {
                return BadRequest(new ApiResponse
                {
                    status = HttpStatusCode.BadRequest.ToString(),
                    message = ApiResponseMessage.NOT_FOUND,
                    data = ""
                });
            }

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK.ToString(),
                message = ApiResponseMessage.SUCCESS,
                data = ""
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _services.DeleteAsync(id);
            if (!success)
            {
                return BadRequest(new ApiResponse
                {
                    status = HttpStatusCode.BadRequest.ToString(),
                    message = ApiResponseMessage.NOT_FOUND,
                    data = ""
                });
            }

            return Ok(new ApiResponse
            {
                status = HttpStatusCode.OK.ToString(),
                message = ApiResponseMessage.SUCCESS,
                data = ""
            });
        }
    }
}
