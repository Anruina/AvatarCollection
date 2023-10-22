using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AvatarCollection.Data;
using Microsoft.EntityFrameworkCore;

namespace AvatarCollectionAPI
{
    public class Program
    {
    
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Avatar Collections API",
                    Description = "Een .Net Core Web API voor Avatar Collections Webpage",
                    TermsOfService = new Uri("https://github.com/Anruina?tab=repositories"),
                    Contact = new OpenApiContact
                    {
                        Name = "Test",
                        Url = new Uri("https://github.com/Anruina?tab=repositories")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Test",
                        Url = new Uri("https://example.com/license")
                    }

                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddDbContext<DataDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default2Connection")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}