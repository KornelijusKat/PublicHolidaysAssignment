using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.PublicHolidayServices
{
    public interface IPublicHolidayService
    {
        List<CountryHoliday> GetPublicHolidays(string year, string countryCode, string region);
        DayStatus CheckDayStatus(string date, string country);
        IEnumerable<SupportedCountry> GetSupportedCountryList();
    }
}
