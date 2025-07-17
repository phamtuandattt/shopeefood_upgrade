using ShopeeFood.BLL.ApplicationServices;
using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood_WebApp.Models.Cities;

namespace ShopeeFood_WebApp.Controllers.Components
{
    [ViewComponent]
    public class FooterViewComponent : ViewComponent
    {
        const string FooterFile = "../../_Footer";

        private IHttpContextAccessor httpContextAccessor;

        public FooterViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            // Handle logic to fetch data

            return View(FooterFile);
        }
    }
}
