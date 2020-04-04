using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TechnologyComplex.Models;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace TechnologyComplex
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
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string HistorianConnection = Configuration.GetConnectionString("HistorianConnection");
            // добавляем контекст CompanyContext в качестве сервиса в приложение
            services.AddDbContext<CompanyContext>(options =>
                  options.UseSqlServer(connection));
            services.AddDbContext<FacilityContext>(options =>
                 options.UseSqlServer(connection));
            services.AddDbContext<Company_FacilityContext>(options =>
                 options.UseSqlServer(connection));
            services.AddDbContext<WorkFlowContext>(options =>
                 options.UseSqlServer(connection));
            services.AddDbContext<Facility_WorkFlowContext>(options =>
                 options.UseSqlServer(connection));
            services.AddDbContext<AreaContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<WorkFlow_AreaContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<UnitContext>(options =>
              options.UseSqlServer(connection));
            services.AddDbContext<Area_UnitContext>(options =>
              options.UseSqlServer(connection));
            services.AddDbContext<EquipmentContext>(options =>
              options.UseSqlServer(connection));
            services.AddDbContext<Motor_ValueContext>(options =>
              options.UseSqlServer(connection));
            services.AddDbContext<Technology_Complex_Context>(options =>
             options.UseSqlServer(connection));
            services.AddDbContext<HistoryValuesContext>(options =>
             options.UseSqlServer(HistorianConnection));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


           

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
            });

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
                app.UseHsts();
            }
           

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("AllowAll");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Facility}/{id?}");
            });
        }
    }
}
