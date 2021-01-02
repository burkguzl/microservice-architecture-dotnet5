using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Phonebook.API.Data.Abstract;
using Phonebook.API.Data.Concrete;
using Phonebook.API.Repositories.Abstract;
using Phonebook.API.Repositories.Concrete;
using Phonebook.API.Settings.Abstract;
using Phonebook.API.Settings.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API
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

            services.Configure<PhonebookDatabaseSettings>(Configuration.GetSection("PhonebookDatabaseSettings"));


            //set into the values of the person database settings inside to the person database settings class
            services.AddSingleton<IPhonebookDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PhonebookDatabaseSettings>>().Value);

            services.AddTransient<IPhonebookDbContext, PhonebookDbContext>();

            services.AddTransient<IPersonRepository, PersonRepository>();

            services.AddTransient<IAddressRepository, AddressRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Phonebook API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Burak Guzel",
                        Email = "burakguzel@outlook.com",
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Phonebook API V1");

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
