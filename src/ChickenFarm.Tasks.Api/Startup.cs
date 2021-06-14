using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ChickenFarm.Tasks.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) =>
            _configuration = configuration;


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddDapr();

            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ChickenFarm.Tasks.Api", Version = "v1"})
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChickenFarm.Tasks.Api v1"))
                .UseRouting()
                .UseCloudEvents()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapSubscribeHandler();
                    endpoints.MapControllers();
                });
        }
    }
}