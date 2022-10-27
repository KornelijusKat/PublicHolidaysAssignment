using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.Repository
{
    public interface ICountryHolidayRepository
    {
        bool AddToDatabase(string body,string country, string region);
        void AddToDayStatusDatabase(DateTime date, string body, string country);
        void AddToCountriesDatabase(List<SupportedCountry> supportedCountries);
    }
}
