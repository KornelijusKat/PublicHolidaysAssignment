using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.HelperMethods;
using PublicHolidaysAssignment.PublicHolidayServices;
using PublicHolidaysAssignment.Repository;

namespace PublicHolidaysAssignment
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
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<HolidayDbContext>(options => options.UseSqlServer("Server=localhost;Database=PublicHolidays;Trusted_Connection=true;"));
            builder.Services.AddScoped<IEnricoApiService, EnricoApiServices>();
            builder.Services.AddScoped<ICountryHolidayRepository, CountryHolidayRepository>();
            builder.Services.AddScoped<IPublicHolidayService, PublicHolidayService>();
            builder.Services.AddScoped<IConsecutiveCounter, ConsecutiveCounter>();
            builder.Services.AddScoped<IJsonDeserializer, JsonDeserializer>();
            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Public Holiday App";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Kornelijus Katinas",
                        Email = "katinaskornelijus@gmail.com",
                        Url = string.Empty,
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    };
                };
            });
          
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials());
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}