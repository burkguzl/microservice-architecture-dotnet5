using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Report.Application.Commands;
using Report.Core.Data;
using Report.Core.Repositories;
using Report.Core.Settings.Abstract;
using Report.Core.Settings.Concrete;
using Report.Infrastructure.Data;
using Report.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Report.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report.API", Version = "v1" });
            });

            services.Configure<PhonebookDatabaseSettings>(Configuration.GetSection("PhonebookDatabaseSettings"));


            //set into the values of the person database settings inside to the person database settings class
            services.AddSingleton<IPhonebookDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PhonebookDatabaseSettings>>().Value);

            services.AddTransient<IPhonebookDbContext, PhonebookDbContext>();

            services.AddTransient<IReportRepository, ReportRepository>();

            services.AddMediatR(typeof(PrepareReportCommand).GetTypeInfo().Assembly);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
