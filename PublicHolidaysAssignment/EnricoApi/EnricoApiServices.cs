using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.Repository;

namespace PublicHolidaysAssignment.EnricoApi
{
    public class EnricoApiServices : IEnricoApiService
    {
        public HttpClient Client;
        private ICountryHolidayRepository _countryHolidayRepository;
        public EnricoApiServices(ICountryHolidayRepository countryHolidayRepository, HttpClient client)
        {
            _countryHolidayRepository = countryHolidayRepository;
            Client = client;
        }
        public List<SupportedCountry> GetSupportedCountries()
        {
            var Enrico = new EnricoApi(Client);
            var result = Enrico.HttpClientExtension($"getSupportedCountries");
            var listas = new List<SupportedCountry>();
            var countryArray = JArray.Parse(result.Result);
            foreach (var item in countryArray)
            {
                var country = JsonConvert.DeserializeObject<SupportedCountry>(item.ToString());
                if (item["regions"] is not null)
                {
                    foreach (var itep in item["regions"])
                    {
                        country.region.Add(itep.ToString());
                    }
                }
                listas.Add(country);
            }
            return listas;
        }
        public ResponseDto<CountryHoliday> GetHolidaysOfGivenCountryAndYear(string year, string country, string region)
        {
            var Enrico = new EnricoApi(Client);
            var countriesWithRegions = new string[] { "nzl", "aus", "can", "usa", "us", "deu", "de", "gbr", "gb" };
            var uriEnding = $"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday";
            if (countriesWithRegions.Contains(country))
            {
                uriEnding = $"getHolidaysForYear&year={year}&country={country}&region={region}&holidayType=public_holiday";
            }
            var result = Enrico.HttpClientExtension(uriEnding);
            if(result.Result.Contains("error"))
            {
                return new ResponseDto<CountryHoliday>() { IsSuccess = false, Message = result.Result };
            }
            _countryHolidayRepository.AddToDatabase(result.Result, country, region);
            return new ResponseDto<CountryHoliday>() { IsSuccess = true , Message = result.Result};
        }
        public ResponseDto<string> SpecificDayStatus(DateTime date, string country)
        {
                var Enrico = new EnricoApi(Client);
                var convertedDate = date.ToString("dd-MM-yyyy");
                var result = Enrico.HttpClientExtension($"isPublicHoliday&date={convertedDate}&country={country}");
                var bools = JObject.Parse(result.Result);
                var thats = bools["isPublicHoliday"];
                if (result.Result.Contains("error"))
                {
                    return new ResponseDto<string>() { IsSuccess = false, Message = result.Result };
                }
                var s = thats.ToObject<bool>();
                if (!s)
                {
                    var result2 = Enrico.HttpClientExtension($"isWorkDay&date={convertedDate}&country={country}");
                    var boolsa = JObject.Parse(result2.Result);
                    var thatsa = boolsa["isWorkDay"];
                    var sa = thatsa.ToObject<bool>();
                    if (!sa)
                    {
                        return new ResponseDto<string>() { Message = "Free day" };
                    }
                    else
                    {
                        return new ResponseDto<string>() { Message = "Work day" };
                    }
                }
                return new ResponseDto<string>() { Message = "Public holiday" };       
        }
    }
}
