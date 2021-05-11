using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestApiCRUDDemo.EmployeeData;
using RestApiCRUDDemo.Model;

namespace RestApiCRUDDemo
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            //Includes a Configure method to create the app's request processing pipeline.
            Configuration = configuration;

            Debug.WriteLine($"CONNECTION STRING: {Configuration.GetSection("ConnectionStrings")["EmployeeContextConnectionString"]}"); 
        }

        // Optionally includes a ConfigureServices method to configure the app's services.
        // A service is a reusable component that provides app functionality.
        // Services are registered in ConfigureServices and consumed across the app via dependency injection (DI) or ApplicationServices.
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
           // This method configures the MVC services for the commonly used features with controllers for an API.
            services.AddControllers();

            #region CORS Policy (Not used)
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "ConsolePolicy",
            //        builder =>
            //        {
            //            builder.WithOrigins("https://localhost:44364", "https://localhost:44364").WithMethods("GET");
            //        });
            //});
            

            #endregion

            /*
             * AddDbContext vs AddDbContextPool
             * DbContext is not thread-safe. So you cannot reuse the same DbContext object for multiple queries at the same time.
             * It (AddDBContextPool) keeps multiple DbContext objects alive and gives you an unused one rather than creating a new one each time.
             */
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeContextConnectionString")));

            //Another valid getter for ConnectionString
            //services.AddDbContextPool<EmployeeContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["EmployeeContextConnectionString"]));

            #region Scoped Explanation

            /*
             * DEPENDECY INJECTION
             * Transient objects are always different; a new instance is provided to every controller and every service.
             * Scoped objects are the same within a request, but different across different requests.
             * Singleton objects are the same for every object and every request.
             */
            //This was used when we de not have a DB connection and the data was hard-coded
            //services.AddSingleton<IEmployeeData, MockEmployeeData>();

            #endregion
            
            services.AddScoped<IEmployeeData, SqlEmployeeData>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestApiCRUDDemo", Version = "v1" });
                
                //Add documentation comments to the API
                //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio#xml-comments
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // The Configure method is used to specify how the app responds to HTTP requests.
        // The request pipeline is configured by adding middleware components to an IApplicationBuilder instance.
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //I want Swagger in production...
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestApiCRUDDemo v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
