using Microsoft.AspNetCore.Mvc;

namespace ShopeeFood_WebApp.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Shops()
        {
            return View();
        }
    }
}
