using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using core_tool_empty.Data;
using Microsoft.EntityFrameworkCore;
using core_tool_empty.DAL;
using core_tool_empty.DALEntity;
using Microsoft.Extensions.Configuration;
using core_tool_empty.ConfigEntity;
using Microsoft.AspNetCore.Identity;

namespace core_tool_empty
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _config = null;
        public Startup(IConfiguration configuration)
        {
            this._config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(option =>
            {
                option.UseSqlServer(this._config.GetConnectionString("Dev"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "152071954153-kl0gc6i8ba2qfghn55fudpprofg4hjc0.apps.googleusercontent.com";
                options.ClientSecret = "k9wQRuez1dR-AmPqdsm7xJwc";
            });

            services.AddScoped<ICrud, Crud>();
            services.AddScoped<IAuthentication, Authentication>();

            services.Configure<AuthorDetails>(this._config.GetSection("AuthorInfo"));
            services.Configure<AuthorDetails>("DeveloperInfo", this._config.GetSection("DeveloperInfo"));
            services.AddControllersWithViews();

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.Use(async (context, next) =>
                {
                    //await context.Request.Headers.Add();
                    await next();
                    //await context.Response.WriteAsync("from response");
                });
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //	endpoints.Map("/name", async c => 
            //	{
            //		await c.Response.WriteAsync("Hello Name");
            //	});
            //});

        }
    }
}
