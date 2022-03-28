using Globus.Data.Domain;
using Globus.Data.DTO;
using Globus.Services.Contract;
using Globus.Services.Handler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globus
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
            var connect = Configuration.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();


            services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connect));
            //services.AddControllersWithViews();
            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            //.AddFluentValidation();

            services.AddTransient<ICustomerRepo<CustomerDTo>, CustomerRepo<CustomerDTo>>();
            services.AddTransient<IOtpRepo, OtpRepo>();


            services.AddRazorPages();
            services.AddCors(setup =>
            {
                // Set up our policy name
                setup.AddPolicy("AllowRoundTheCode", policy =>
                {
                    policy.WithOrigins(new string[] { "https://localhost:44331" }).AllowAnyMethod().AllowAnyHeader().AllowCredentials(); // Allow everyone from https://www.roundthecode.com (you're very kind)
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            }).AddSwaggerGenNewtonsoftSupport();


           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApi v1");
                c.InjectStylesheet("/swagger/custom.css");
                c.RoutePrefix = String.Empty;
            });


            app.UseAuthorization();

            app.UseCors("AllowRoundTheCode");



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=Customers}/{id?}");
            });
        }
    }
}
