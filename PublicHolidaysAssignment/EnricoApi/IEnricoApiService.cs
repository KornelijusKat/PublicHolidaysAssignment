using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.EnricoApi
{
    public interface IEnricoApiService
    {
        Task<string> GetHolidaysOfGivenCountryAndYear(string year, string country, string region);
        string SpecificDayStatus(string date, string country);
        List<SupportedCountry> GetSupportedCountries();
    }
}
