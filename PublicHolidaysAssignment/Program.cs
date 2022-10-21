using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PublicHolidaysAssignment.EnricoApi;
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
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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