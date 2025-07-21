using Microsoft.AspNetCore.Mvc;

namespace ShopeeFood_WebApp.Controllers
{
    public class CategoryController : Controller
    {
        [Route("Category")]
        [Route("/category/shops")]
        public IActionResult Shops(int id)
        {
            ViewBag.ActiveCategoryId = id;
            ViewBag.PageTitle = "Category";

            return View();
        }
    }
}
