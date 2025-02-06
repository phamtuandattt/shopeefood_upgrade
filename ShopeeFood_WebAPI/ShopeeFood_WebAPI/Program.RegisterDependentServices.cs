using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.ApplicationServices;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.BLL.Servives;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.IRepositories.ICityRepository;
using ShopeeFood_WebAPI.DAL.IRepositories.IShopRepository;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.DAL.Repositories;
using ShopeeFood_WebAPI.DAL.Repositories.CityRepository;
using ShopeeFood_WebAPI.DAL.Repositories.ShopRepository;
using System.Reflection;

public static class RegisterDependentServices
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        var configLog4netPath = builder.Configuration["log4net"] ?? "";
        // Configure log4net
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo(configLog4netPath));

        builder.Services.AddDbContext<ShopeefoodDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ShopeeFoodDbContext"));
            options.EnableSensitiveDataLogging();
        });
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        // Repository
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<IShopRepository, ShopRepository>();


        // Services
        builder.Services.AddScoped<ICityServies, CityServices>();
        builder.Services.AddScoped<IShopServices, ShopServices>();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
