using System;
using CalendarApi.BusinessLayer.Mappers;
using CalendarApi.BusinessLayer.Providers;
using CalendarApi.BusinessLayer.Services;
using CalendarApi.BusinessLayer.Settings;
using CalendarApi.BusinessLayer.Validations;
using CalendarApi.DataAccessLayer;
using CalendarApi.Serialization;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace CalendarApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configure<AppSettings>(nameof(AppSettings));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EventRequestValidator>());

            services.AddAutoMapper(typeof(EventMapperProfile).Assembly);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar API", Version = "v1" });

                options.MapType<FileContentResult>(() => new OpenApiSchema
                {
                    Type = "file"
                });

                options.MapType<DateTime>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"))
                });
            });

            services.AddDbContext<CalendarDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"), providerOptions =>
                {
                    providerOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null);
                });
            });

            services.AddHostedService<ApplicationStartupTask>();

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IAttachmentService, AttachmentService>();

            if (appSettings.IsRunningOnAzure)
            {
                services.AddAzureStorage(options =>
                {
                    options.ConnectionString = Configuration.GetConnectionString("AzureStorageConnectionString");
                    options.ContainerName = "attachments";
                });
            }
            else
            {
                services.AddFileSystemStorage(options =>
               {
                   options.StorageFolder = appSettings.StorageFolder;
               });
            }

            services.AddProblemDetails();

            T Configure<T>(string sectionName) where T : class
            {
                var section = Configuration.GetSection(sectionName);
                var settings = section.Get<T>();
                services.Configure<T>(section);

                return settings;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API v1");
                options.RoutePrefix = string.Empty;
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
