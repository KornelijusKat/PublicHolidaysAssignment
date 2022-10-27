using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.PublicHolidayServices
{
    public interface IPublicHolidayService
    {
        ResponseDto<CountryHoliday> GetPublicHolidays(string year, string countryCode, string region);
        ResponseDto<DayStatus> CheckDayStatus(DateTime date, string country);
        ResponseDto<SupportedCountry> GetSupportedCountryList();
        ResponseDto<int> GetConsecutive(string country,string year, string region);
    }
}
