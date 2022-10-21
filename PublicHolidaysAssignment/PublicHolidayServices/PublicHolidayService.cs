using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.Extensions;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.Repository;
using System.Diagnostics.Metrics;

namespace PublicHolidaysAssignment.PublicHolidayServices
{
    public class PublicHolidayService :IPublicHolidayService
    {
        private readonly IEnricoApiService _enricoApiService;
        private readonly ICountryHolidayRepository _countryHolidayRepository;
        private readonly HolidayDbContext _dbContext;
        public PublicHolidayService(ICountryHolidayRepository countryHolidayRepository, IEnricoApiService enricoApiService, HolidayDbContext dbContext)
        {
            _enricoApiService = enricoApiService;
            _countryHolidayRepository = countryHolidayRepository;
            _dbContext = dbContext;
        }
        public List<CountryHoliday> GetPublicHolidays(string year, string countryCode, string region)
        {
            var hello = _dbContext.Holidays.FirstOrDefault(x => (x.CountryCode == countryCode) && (x.Year == int.Parse(year)));
            if(hello is null)
            {
               var result = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year, countryCode, region);
                var deserialized = JsonConvert.DeserializeObject<List<Root>>(result.Result);
                var hellos = new List<CountryHoliday>();
                if (region != null)
                {
                    foreach (var item in deserialized)
                    {
                        var newCountry = new CountryHoliday() { CountryCode = countryCode, Day = item.date.day, DayOfWeek = item.date.dayOfWeek, Year = item.date.year, Month = item.date.month, HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text };
                        hellos.Add(newCountry);
                    }
                }
                else
                {
                    foreach (var item in deserialized)
                    {
                        var newCountry = new CountryHoliday() { CountryCode = countryCode, Day = item.date.day, DayOfWeek = item.date.dayOfWeek, Year = item.date.year, Month = item.date.month, HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, LangEn = item.name[1].lang, TextEn = item.name[1].text };
                        hellos.Add(newCountry);
                    }
                }
                return hellos;
            }       
             var result2 = _dbContext.Holidays.Where(x => (x.CountryCode == countryCode) && (x.Year == int.Parse(year))).ToList();
             var newJson = JsonConvert.SerializeObject(result2);
             return result2;
        }
        public DayStatus CheckDayStatus(string date, string country)
        {
            var hello = _dbContext.DayStatuses.FirstOrDefault(x => (x.CountryCode == country) && (x.Date == date));
            if (hello is null)
            {
              var result = _enricoApiService.SpecificDayStatus(date, country);
                var newDay = new DayStatus() { CountryCode = country, TypeOfDay = result, Date = date };
                return newDay;
            }
            var newJson = JsonConvert.SerializeObject(hello.TypeOfDay);
            return hello;
        }
        public IEnumerable<SupportedCountry> GetSupportedCountryList()
        {
            if (!_dbContext.Countries.Any())
            {
                var result = _enricoApiService.GetSupportedCountries();
                _countryHolidayRepository.AddToCountriesDatabase(result);
                
                return result;
            }
            var queryResult = _dbContext.Countries.AsEnumerable();
            return queryResult.maps();
        }
    }
}
