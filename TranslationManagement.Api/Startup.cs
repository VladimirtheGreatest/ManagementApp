using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TranslationManagement.Data;
using TranslationManagement.Repository.Contracts;
using TranslationManagement.Services.Contracts;
using TranslationManagement.Services;
using TranslationManagement.Api.CustomExceptionMiddleware;

namespace TranslationManagement.Api
{
    public class Startup
    {
        private string _connection = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            //DI
            services.AddScoped<IRepository, TranslationManagement.Repository.Repository>();
            services.AddScoped<ITranslatorManagementService, TranslatorManagementService>();
            services.AddScoped<ITranslationJobService, TranslationJobService>();
            services.AddScoped<IPricingService, PricingService>();
            services.AddScoped<IFileService, FileService>();

            //only local environment, for production get the value from appconfig/keyvault;
            _connection = Configuration.GetConnectionString("Default");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TranslationManagement.Api", Version = "v1" });
            });

            //EF
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_connection, providerOptions => providerOptions.EnableRetryOnFailure());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TranslationManagement.Api v1"));

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
