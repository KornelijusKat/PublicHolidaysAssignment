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

        public Task<string> GetHolidaysOfGivenCountryAndYear(string year, string country)
        {
            var Enrico = new EnricoApi(Client);
            var result = Enrico.HttpClientExtension($"getHolidaysForYear&year={year}&country={country}&holidayType=public_holiday");
            _countryHolidayRepository.AddToDatabase(result.Result, country);
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
                    return "Free day";
                }
                else
                {
                    return "Work day";
                }
            }
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
