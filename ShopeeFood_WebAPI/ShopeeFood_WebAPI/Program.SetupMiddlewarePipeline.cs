public static class SetupMiddlewarePipeline
{
    public static WebApplication SetupMiddleware(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        //app.UseStaticFiles();

        //app.UseSession();

        /// for developer
        //if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        //{
            //app.UseSwagger();
            //app.UseSwaggerUI();
        //}

        // for deploy IIS
        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        return app;
    }
}