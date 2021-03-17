using AutoMapper;
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
using SurveyMidterm.Data;
using SurveyMidterm.Infrastructure;
using SurveyMidterm.Models.Profiles;
using SurveyMidterm.Services.Abstractions;
using SurveyMidterm.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyMidterm
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

            services.AddControllers();
            services.AddDbContext<SurveyDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>().DefaultConnection,
                    optionsBuilder =>
                    {
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(60);
                        optionsBuilder.MigrationsAssembly("SurveyMidterm.Data");
                    });
                options
                    .UseInternalServiceProvider(serviceProvider)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }).AddEntityFrameworkSqlServer();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AnswerProfile));
            }).CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<ISurveyUserService, SurveyUserService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SurveyMidterm", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyMidterm v1"));
            }

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
