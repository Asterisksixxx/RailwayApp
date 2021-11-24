using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Data;
using RailwayApp.Services;

namespace RailwayApp
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
            services.AddControllersWithViews();
            string conString = "";
            switch (Environment.MachineName)
            {
                case "DESKTOP-KVLB56K":
                    conString = "DCDESKTOP-KVLB56K";
                    break;
                case "ASTERISK_PC":
                    conString = "DCASTERISK_PC";
                    break;
                default:
                    conString = "DCDESKTOP-KVLB56K";
                    break;
            }
            services.AddDbContext<AppDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(conString)));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/User/Login");
                });
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IStationService,StationService>();
            services.AddTransient<ITrainService, TrainService>();
            services.AddTransient<IRouteService, RouteService>();

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
