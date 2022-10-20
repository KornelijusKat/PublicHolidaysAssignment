using Newtonsoft.Json;
using PublicHolidaysAssignment.Repository;

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
        public string GetPublicHolidays(string year, string countryCode)
        {
            var hello = _dbContext.Holidays.FirstOrDefault(x => (x.CountryCode == countryCode) && (x.Year == int.Parse(year)));
            if(hello is null)
            {
               var result = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year, countryCode);
                return result.Result;
            }       
             var result2 = _dbContext.Holidays.Where(x => (x.CountryCode == countryCode) && (x.Year == int.Parse(year))).ToList();
             var newJson = JsonConvert.SerializeObject(result2);
             return newJson;
        }
    }
}
