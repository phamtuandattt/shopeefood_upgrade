using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ShopeeFood_WebApp.Helpers
{
    public static class MenuHelper
    {
        public static string IsActive(ViewContext viewContext, string controller, string action = null)
        {
            var currentController = viewContext.RouteData.Values["controller"]?.ToString();
            var currentAction = viewContext.RouteData.Values["action"]?.ToString();

            bool isActive = string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase)
                            && (string.IsNullOrEmpty(action) || string.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase));

            return isActive ? "active" : string.Empty;
        }

        //@inject Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper HtmlHelper
        //@{
        //            var viewContext = ViewContext;
        //        }

        //<div class="nav-links" id="menu">
        //    <a class="nav-link-item @MenuHelper.IsActive(viewContext, "Home", "Index")" href="/">Home</a>
        //    <a class="nav-link-item @MenuHelper.IsActive(viewContext, "Category", "Shops")" href="/category/shops">Categories</a>
        //    <a class="nav-link-item @MenuHelper.IsActive(viewContext, "Contact")" href="/contact">Contact</a>
        //</div>

        public static string IsActive(int currentId, int expectedId)
        {
            return currentId == expectedId ? "active" : "";
        }
    }
}
