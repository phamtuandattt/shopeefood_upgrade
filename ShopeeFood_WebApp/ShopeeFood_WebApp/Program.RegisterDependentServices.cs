namespace ShopeeFood_WebApp
{
    public static class RegisterDependentServices
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            return builder;
        }
    }
}
