using Microsoft.AspNetCore.Mvc.Razor;

namespace ShopeeFood_WebApp
{
    public class ViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // {0} = View Name(Index)
            // {1} = Controller Name(Home)
            
            // Add theme location to the end of the ViewLocations list
            //var themeDir = (string?)context.ActionContext.HttpContext.Items["ThemeDir"];
            var themeDir = "ThemeV1";

            string[] locations = new string[] {
                "/Views/" + themeDir + "/Shared/Components/{0}.cshtml",
                "/Views/" + themeDir + "/Shared/Layout/{0}.cshtml",
                "/Views/" + themeDir + "/Page/{0}.cshtml",
                "/Views/" + themeDir + "/Module/{1}/{0}.cshtml",
                "/Views/" + themeDir + "/Navigation/{0}.cshtml",
                "/Views/" + themeDir + "/Error/{0}.cshtml",
                "/Views/" + themeDir + "/{1}/{0}.cshtml",   
                "/Views/" + themeDir + "/{0}.cshtml"
            };

            return viewLocations.Concat(locations);
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // Nothing to do
        }
    }
}
