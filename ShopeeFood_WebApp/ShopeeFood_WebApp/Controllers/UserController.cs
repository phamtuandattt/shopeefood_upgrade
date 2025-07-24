using Microsoft.AspNetCore.Mvc;

namespace ShopeeFood_WebApp.Controllers
{
    public class UserController : Controller
    {
        [Route("/login")]
        public ActionResult LoginModule()
        {

            return View();
        }
    }
}
