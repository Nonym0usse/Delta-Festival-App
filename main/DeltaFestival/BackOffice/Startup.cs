using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackOffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors();
            services.AddMvcCore(options =>
              {
              });
            services.AddDbContext<EfContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("FestivalDb")
                    , x => x.MigrationsAssembly("Database")));
       
            //AutoMapper
            services.AddAutoMapper();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Services Enabled by services.AddMVC
            
            var builder = services.AddMvcCore(); //set of core services of the mvc model including routing and controllers
            builder.AddApiExplorer(); //Service responsible for gathering and exposing information about controllers and actions for dynamic discovery of capabilities and help pages
            builder.AddAuthorization(); //Service behind authentification and authorization
            builder.AddViews(); //Service to process action results as HTML views 
            builder.AddApiExplorer();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
             ////  app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
