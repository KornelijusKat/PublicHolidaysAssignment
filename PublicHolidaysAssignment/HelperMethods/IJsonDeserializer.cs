using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.HelperMethods
{
    public interface IJsonDeserializer
    {
        IEnumerable<CountryHoliday> CountryHolidayDeserializer(string countryCode, ResponseDto<CountryHoliday> message);
    }
}
