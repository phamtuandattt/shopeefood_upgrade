using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood_WebApp.Models.Cities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopeeFood_WebApp.Controllers.Components
{
    [ViewComponent]
    public class HeaderViewComponent : ViewComponent
    {
        const string HeaderFile = "../../_Header"; 

        private readonly IBusinessServices businessServices;
        private IHttpContextAccessor httpContextAccessor;

        public HeaderViewComponent(IBusinessServices businessServices, IHttpContextAccessor httpContextAccessor)
        {
            this.businessServices = businessServices;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync(int cityId)
        {
            var cities = await businessServices.GetAllByCity(httpContextAccessor.HttpContext);
            var business = await businessServices.GetBusinessByCity(httpContextAccessor.HttpContext, 1);

            var viewModel = new CityViewModel();

            foreach (var item in cities.Data)
            {
                viewModel.Cities.Add(new CityModel()
                {
                    CityId = item.CityId,
                    CityName = item.CityName,
                });
            }

            foreach (var item in business.Data)
            {
                viewModel.CityBusinesses.Add(new CityBusinessModel()
                {
                    CitiId = item.CityID,
                    FieldName = item.FieldName,
                });
            }

            return View(HeaderFile, viewModel);
        }
    }
}


