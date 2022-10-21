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
        public void AddToDatabase(string body,string country, string region)
        {
            var deserialized = JsonConvert.DeserializeObject<List<Root>>(body);
            if (region != null)
            {
                foreach (var item in deserialized)
                {
                    var newCountry = new CountryHoliday() { CountryCode = country, Day = item.date.day, DayOfWeek = item.date.dayOfWeek, Year = item.date.year, Month = item.date.month, HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text };
                    _context.Holidays.Add(newCountry);
                }
            }
            else
            {
                foreach (var item in deserialized)
                {
                    var newCountry = new CountryHoliday() { CountryCode = country, Day = item.date.day, DayOfWeek = item.date.dayOfWeek, Year = item.date.year, Month = item.date.month, HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, LangEn = item.name[1].lang, TextEn = item.name[1].text };
                    _context.Holidays.Add(newCountry);
                }
            }
            _context.SaveChanges();
        }
        public void AddToDayStatusDatabase(string date, string body, string country)
        {
            var newDay = new DayStatus() { CountryCode = country, TypeOfDay = body, Date = date };
            _context.DayStatuses.Add(newDay);
            _context.SaveChanges();
        }
        public void AddToCountriesDatabase(List<SupportedCountry> supportedCountries)
        {
            foreach(var item in supportedCountries)
            {
                var newCountry = new Country() { countryCode = item.countryCode, fullName = item.fullName };
                if (item.region != null)
                {
                    foreach (var region in item.region)
                    {
                        var newRegion = new Region() { CountryCode = item.countryCode, Name = region };
                        newCountry.region.Add(newRegion);
                    }
                }
            }
        }
    }
}
