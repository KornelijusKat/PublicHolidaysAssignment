using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.Repository
{
    public interface ICountryHolidayRepository
    {
        void AddToDatabase(string body,string country, string region);
        void AddToDayStatusDatabase(string date, string body, string country);
        void AddToCountriesDatabase(List<SupportedCountry> supportedCountries);
    }
}
