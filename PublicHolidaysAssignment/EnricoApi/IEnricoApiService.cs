using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.EnricoApi
{
    public interface IEnricoApiService
    {
        ResponseDto<CountryHoliday> GetHolidaysOfGivenCountryAndYear(string year, string country, string region);
        ResponseDto<string> SpecificDayStatus(DateTime date, string country);
        List<SupportedCountry> GetSupportedCountries();
    }
}
