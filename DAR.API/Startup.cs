using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAR.API.Infrastructure;
using DAR.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using Swashbuckle.AspNetCore.Swagger;

namespace DAR.API
{
    public class Startup
    {
        private Container container = new Container();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters();
            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                .AddControllerActivation();
            });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Title = "DAR API",
                    Version = "V1"
                });
            });
            services.AddDbContext<DiagramContext>(options => options.UseSqlite(ReadConnectionString()));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            InitializeContainer(app);
            container.Verify();

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "DAR.API V1");

            });
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            container.Register<IDiagramService, ARDService>(Lifestyle.Scoped);

            // container.Register<DiagramContext>(() =>
            // {
            //     var optionsBuilder = new DbContextOptionsBuilder<DiagramContext>().UseSqlite(ReadConnectionString());
            //     return new DiagramContext(optionsBuilder.Options);
            // }, Lifestyle.Scoped);
            container.Register(app.ApplicationServices.GetService<DiagramContext>, Lifestyle.Scoped);


        }

        private string ReadConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }
    }

}
