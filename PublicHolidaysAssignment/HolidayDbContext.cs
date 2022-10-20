using Microsoft.EntityFrameworkCore;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment
{
    public class HolidayDbContext :DbContext
    {
        public DbSet<CountryHoliday> Holidays  { get; set; }
        public DbSet<DayStatus> DayStatuses { get; set; }
        public HolidayDbContext(DbContextOptions<HolidayDbContext> options) : base(options)
        { 

        }
    }
}
