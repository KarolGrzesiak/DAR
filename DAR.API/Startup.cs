using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CamundaClient;
using DAR.API.Helpers;
using DAR.API.Infrastructure;
using DAR.API.Model.BPM;
using DAR.API.Model.DMN;
using DAR.API.Model;
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
using Microsoft.OpenApi.Models;
using SimpleInjector;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using DAR.API.Infrastructure.Settings;

namespace DAR.API
{
    public class Startup
    {
        private Container _container = new Container();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _container.Options.ResolveUnregisteredConcreteTypes = false;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddXmlSerializerFormatters().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                   ReferenceLoopHandling.Ignore;
            });
            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                .AddControllerActivation();
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DAR API",
                    Version = "V1"
                });
            });
            services.AddDbContextPool<DiagramContext>(options => options.UseSqlite(ReadConnectionString()));
            InitializeContainer();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSimpleInjector(_container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "DAR.API V1");
            });


            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials().
                WithExposedHeaders("Location"));
            app.UseHttpsRedirection();

            app.UseMvcWithDefaultRoute();
            _container.Verify();


        }

        private void InitializeContainer()
        {

            RegisterServices();
            RegisterHelpers();
            RegisterCamunda();
        }



        private void RegisterCamunda()
        {
            _container.Register<CamundaEngineClient>(() => new CamundaEngineClient(new Uri(ReadCamundaRESTAddress()), null, null), Lifestyle.Scoped);
            _container.Register<CamundaSettings>(() => new CamundaSettings()
            {
                Address = ReadCamundaUIAddress()
            }, Lifestyle.Singleton);
        }

        private void RegisterHelpers()
        {
            Assembly assembly = typeof(BPMModeler).Assembly;
            var types = assembly.GetTypes().Where(t => t.IsClass && t.Namespace == typeof(BPMModeler).Namespace);
            foreach (var type in types)
            {
                _container.Register(type, type, Lifestyle.Scoped);
            }
        }

        private void RegisterServices()
        {
            Assembly assembly = typeof(IBPMService).Assembly;
            var serviceTypes = assembly.GetTypes().Where(t => !t.IsAbstract && t.Name.EndsWith("Service") && t.Namespace == typeof(IBPMService).Namespace);
            System.Console.WriteLine(serviceTypes);
            foreach (var type in serviceTypes)
            {
                _container.Register(type.GetInterfaces().Single(), type, Lifestyle.Scoped);
            }

        }

        private string ReadConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnectionString");
        }
        private string ReadCamundaRESTAddress()
        {
            return Configuration.GetValue<string>("CamundaRESTAddress");
        }
        private string ReadCamundaUIAddress()
        {
            return Configuration.GetValue<string>("CamundaUIAddress");

        }
    }

}
