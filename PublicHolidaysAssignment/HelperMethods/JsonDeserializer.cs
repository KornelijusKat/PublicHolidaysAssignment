using Newtonsoft.Json;
using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PublicHolidaysAssignment.HelperMethods
{
    public class JsonDeserializer :IJsonDeserializer
    {
        public IEnumerable<CountryHoliday> CountryHolidayDeserializer(string countryCode,ResponseDto<CountryHoliday> message )
        {
            var deserialized = JsonConvert.DeserializeObject<List<Root>>(message.Message);
            var countriesWithRegions = new string[] { "nzl", "aus", "can", "usa", "us", "deu", "de", "gbr", "gb" };
            var tempList = new List<CountryHoliday>();
            if (countriesWithRegions.Contains(countryCode))
            {
                foreach (var item in deserialized)
                {
                    var newCountry = new CountryHoliday() { CountryCode = countryCode, Date = new DateTime(item.date.year, item.date.month, item.date.day), HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text };
                    tempList.Add(newCountry);
                }
            }
            else
            {
                foreach (var item in deserialized)
                {
                    var newCountry = new CountryHoliday() { CountryCode = countryCode, DayOfWeek = item.date.dayOfWeek, Date = new DateTime(item.date.year, item.date.month, item.date.day), HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, LangEn = item.name[1].lang, TextEn = item.name[1].text };
                    tempList.Add(newCountry);
                }
            }
            return tempList;
        }
    }
}
