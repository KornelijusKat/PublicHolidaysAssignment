using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.Repository
{
    public interface ICountryHolidayRepository
    {
        bool AddToDatabase(string body,string country, string region);
        void AddToDayStatusDatabase(DateTime date, string body, string country);
        void AddToCountriesDatabase(List<SupportedCountry> supportedCountries);
        DayStatus QueryDayRecord(string country, DateTime date);
        IEnumerable<CountryHoliday> GetOrderedList(string countryCode, string year, string region);
        CountryHoliday QueryIfCountryHolidayExists(string country, string year, string region);
        bool QueryIfAnyRecordExists();
        IEnumerable<Country> GetSupportedCountriesAndTheirRegions();

    }
}
