using Newtonsoft.Json;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.Repository
{
    public class CountryHolidayRepository :ICountryHolidayRepository
    {
        private readonly HolidayDbContext _context; 
        public CountryHolidayRepository(HolidayDbContext context)
        {
            _context = context;
        }
        public void AddToDatabase(string body,string country)
        {
            var deserialized = JsonConvert.DeserializeObject<List<Root>>(body);
            foreach (var item in deserialized)
            {
                var newCountry = new CountryHoliday() { CountryCode = country, Day = item.date.day, DayOfWeek = item.date.dayOfWeek, Year = item.date.year, Month = item.date.month, HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, LangEn = item.name[1].lang, TextEn = item.name[1].text };
                _context.Holidays.Add(newCountry);
            }
            _context.SaveChanges();
        }
    }
}
