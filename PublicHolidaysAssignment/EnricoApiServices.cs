using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.Repository;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace PublicHolidaysAssignment
{
    public class EnricoApiServices :IEnricoApiService
    {
        public HttpClient Client = new HttpClient();
        private ICountryHolidayRepository _countryHolidayRepository;
        public EnricoApiServices(ICountryHolidayRepository countryHolidayRepository)
        {
            _countryHolidayRepository = countryHolidayRepository;
        }
        public List<SupportedCountry> GetSupportedCountries()
        {
            var Enrico = new EnricoApi(Client);
            var result = Enrico.HttpClientExtension($"getSupportedCountries");
            var listas = new List<SupportedCountry>();
            var bools = JArray.Parse(result.Result);
            foreach (var item in bools)
            {
                var help = JsonConvert.DeserializeObject<SupportedCountry>(item.ToString());
                
                if (item["regions"] is not null)
                {
                    foreach (var itep in item["regions"])
                    {
                        help.region.Add(itep.ToString());
                    }
                }
                listas.Add(help);
            }
            return listas;
        }
        public Task<string> GetHolidaysOfGivenCountryAndYear(string year, string country, string region)
        {
            var Enrico = new EnricoApi(Client);
            var uriEnding = $"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday";
            if(region != null)
            {
                uriEnding = $"getHolidaysForYear&year={year}&country={country}&region={region}&holidayType=public_holiday";
            }
            var result = Enrico.HttpClientExtension(uriEnding);
            _countryHolidayRepository.AddToDatabase(result.Result, country, region);
            return result;
        }
        public string SpecificDayStatus(string date, string country)
        {       
            var Enrico = new EnricoApi(Client);
            var result = Enrico.HttpClientExtension($"isPublicHoliday&date={date}&country={country}");
            var bools = JObject.Parse(result.Result);
            var thats = bools["isPublicHoliday"];
            var s = thats.ToObject<bool>();
            if(!s)
            {
                var result2 = Enrico.HttpClientExtension($"isWorkDay&date={date}&country={country}");
                var boolsa = JObject.Parse(result2.Result);
                var thatsa = boolsa["isWorkDay"];
                var sa = thatsa.ToObject<bool>();
                if(!sa)
                {
                    _countryHolidayRepository.AddToDayStatusDatabase(date, "Free day", country);           
                    return "Free day";
                }
                else
                {
                    _countryHolidayRepository.AddToDayStatusDatabase(date, "Work day", country);
                    return "Work day";
                }
            }
            _countryHolidayRepository.AddToDayStatusDatabase(date, "Public ", country);
            return "Public holiday";
        }
        //public string CountMostDayOffs(string year, string country)
        //{
        //    var Enrico = new EnricoApi(Client);
        //    var result = Enrico.HttpClientExtension($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
        //    var deserialized = JsonConvert.DeserializeObject<Root>(result.Result);
        //    var kekw = deserialized.Date;
        //    var pro = new List<DateTime>();
        //    var count = 0;
        //    foreach (var day in kekw)
        //    {
        //        DateTime Joe = DateTime.Parse(day.Day + day.Month + day.Year);
        //        pro.Add(Joe);
        //    }
        //    pro.Sort();

        //}
    }
}
