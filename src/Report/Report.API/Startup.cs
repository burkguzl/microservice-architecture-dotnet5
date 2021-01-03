using EventBusRabbitMQ.Abstract;
using EventBusRabbitMQ.Concrete;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Report.API.Extensions;
using Report.API.RabbitMQ;
using Report.Application.Queries;
using Report.Core.Data;
using Report.Core.Repositories;
using Report.Core.Settings.Abstract;
using Report.Core.Settings.Concrete;
using Report.Infrastructure.Data;
using Report.Infrastructure.Repositories;
using System.Reflection;

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
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Report API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Burak Guzel",
                        Email = "burakguzel@outlook.com",
                    }
                });
            });

            services.Configure<PhonebookDatabaseSettings>(Configuration.GetSection("PhonebookDatabaseSettings"));


            //set into the values of the person database settings inside to the person database settings class
            services.AddSingleton<IPhonebookDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PhonebookDatabaseSettings>>().Value);

            services.AddTransient<IPhonebookDbContext, PhonebookDbContext>();

            services.AddTransient<IReportRepository, ReportRepository>();

            services.AddMediatR(typeof(GetReportByIdQuery).GetTypeInfo().Assembly);

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"],
                    UserName = Configuration["EventBus:UserName"],
                    Password = Configuration["EventBus:Password"],
                };

                return new RabbitMQConnection(connectionFactory);
            });

            services.AddSingleton<EventBusRabbitMQConsumer>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report API v1"));
            }

            app.UseRabbitListener();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
