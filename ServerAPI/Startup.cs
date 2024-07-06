using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerAPI.Data;
using ServerAPI.Configurations;

namespace ServerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register the database context
            services.AddDbContext<PCInfoContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("PCInfoDatabase")));

            services.AddControllers();

            // Register Swagger services
            services.ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Production settings
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Apply migrations automatically (if needed)
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PCInfoContext>();
                dbContext.Database.Migrate();
            }

            app.UseRouting();

            // Use custom Swagger configuration
            app.UseCustomSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
