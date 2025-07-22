using log4net.Config;
using log4net;
using ShopeeFood.Infrastructure.Logging;
using System.Reflection;
using ShopeeFood.BLL.ServicesContract.BusinessServicesContract;
using ShopeeFood.BLL.ApplicationServices;
using ShopeeFood.Infrastructure.Common.ApiServices;
using Microsoft.AspNetCore.Mvc.Razor;
using ShopeeFood.BLL.ServicesContract.ShopServicesContract;
using ShopeeFood_WebApp.Services;

namespace ShopeeFood_WebApp
{
    public static class RegisterDependentServices
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            // Configure log4net
            var configLog4netPath = builder.Configuration["log4net"] ?? "";
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(configLog4netPath));


            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            
            
            builder.Services.AddScoped<RestServices>();
            builder.Services.AddScoped<ModuleContentSerivces>();
            
            
            builder.Services.AddTransient<IBusinessServices, BusinessServices>();
            builder.Services.AddTransient<IShopServices, ShopServices>();


            builder.Services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationExpanders.Add(new ViewLocationExpander());
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            return builder;
        }
    }
}
