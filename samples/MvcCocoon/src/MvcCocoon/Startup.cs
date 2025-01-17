using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCocoon.Data;
using MvcCocoon.Services;
using ReCode.Cocoon.Proxy.Authentication;
using ReCode.Cocoon.Proxy.Cookies;
using ReCode.Cocoon.Proxy.Proxy;

namespace MvcCocoon
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
            services.AddCocoonSession();
            services.AddCocoonCookies();

            services.AddAuthentication(CocoonAuthenticationDefaults.Scheme).AddCocoon();

            services.AddControllersWithViews();
            services.AddCocoonProxy(Configuration);
            services.AddHostedService<ActivityDebug>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapCocoonProxy();
            });
        }

    }
}
