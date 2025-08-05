using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopeeFood_WebAPI.ApplicationServices;
using ShopeeFood_WebAPI.BLL.Dtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.BLL.Servives;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.IRepositories.ICityRepository;
using ShopeeFood_WebAPI.DAL.IRepositories.ICustomerRepository;
using ShopeeFood_WebAPI.DAL.IRepositories.IShopRepository;
using ShopeeFood_WebAPI.DAL.Models;
using ShopeeFood_WebAPI.DAL.Repositories;
using ShopeeFood_WebAPI.DAL.Repositories.CityRepository;
using ShopeeFood_WebAPI.DAL.Repositories.CustomerRepository;
using ShopeeFood_WebAPI.DAL.Repositories.ShopRepository;
using ShopeeFood_WebAPI.Infrastructure.Common.Email;
using System.Reflection;
using System.Text;

public static class RegisterDependentServices
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        // Load appsetting file for each environment
        var appSettingFile = "appsettings.json";
        #if DEBUG
        appSettingFile = "appsettings.Development.json";
        #endif

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(appSettingFile, false, true)
            .AddEnvironmentVariables();
        //--------------------------------------------

        var configLog4netPath = builder.Configuration["log4net"] ?? "";
        // Configure log4net
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo(configLog4netPath));

        builder.Services.AddDbContext<ShopeefoodDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ShopeeFoodDbContext"));
            options.EnableSensitiveDataLogging();
        });

        // Bind JWT settings
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("Jwt"));

        // Load it as an instance too (optional)
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

        //builder.Services.AddAuthentication("Bearer")
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddScoped<TokenService>();

        // Repository
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<IShopRepository, ShopRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


        // Services
        builder.Services.AddScoped<ICityServies, CityServices>();
        builder.Services.AddScoped<IShopServices, ShopServices>();
        builder.Services.AddScoped<ICustomerServices, CustomerServices>();

        // Get email setting
        //var eS = builder.Configuration.GetSection("EmailSettings");
        //builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddScoped<IEmailServices, EmailServices>();


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}
