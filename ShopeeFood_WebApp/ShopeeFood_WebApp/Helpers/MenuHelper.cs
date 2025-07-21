using Microsoft.AspNetCore.Mvc.Rendering;

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

        public static string IsActive(int currentId, int expectedId)
        {
            return currentId == expectedId ? "active" : "";
        }
    }
}
