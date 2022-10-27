using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.Extensions;
using PublicHolidaysAssignment.ModelDtos;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.Repository;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace PublicHolidaysAssignment.PublicHolidayServices
{
    public class PublicHolidayService :IPublicHolidayService
    {
        private readonly IEnricoApiService _enricoApiService;
        private readonly ICountryHolidayRepository _countryHolidayRepository;
        private readonly HolidayDbContext _dbContext;
        public PublicHolidayService(ICountryHolidayRepository countryHolidayRepository, IEnricoApiService enricoApiService, HolidayDbContext dbContext)
        {
            _enricoApiService = enricoApiService;
            _countryHolidayRepository = countryHolidayRepository;
            _dbContext = dbContext;
        }
        public ResponseDto<CountryHoliday> GetPublicHolidays(string year, string countryCode, string region)
        {
            var response = new ResponseDto<CountryHoliday>();
            var recordsExists = _dbContext.Holidays.FirstOrDefault(x => (x.CountryCode == countryCode) && (x.Date.Year == int.Parse(year)) && (x.Region == region));
            if(recordsExists is null)
            {
                var result = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year, countryCode, region);
                if(!result.IsSuccess)
                {
                    return result;
                }
                var deserialized = JsonConvert.DeserializeObject<List<Root>>(result.Message);
                var countriesWithRegions = new string[] {"nzl", "aus", "can", "usa","us", "deu","de", "gbr", "gb"};
                var tempList = new List<CountryHoliday>();
                if (countriesWithRegions.Contains(countryCode))
                {
                    foreach (var item in deserialized)
                    {
                        var newCountry = new CountryHoliday() { CountryCode = countryCode, Date  = new DateTime(item.date.year, item.date.month, item.date.day), HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text };
                        tempList.Add(newCountry);
                    }
                }
                else
                {
                    foreach (var item in deserialized)
                    {
                        var newCountry = new CountryHoliday() { CountryCode = countryCode,DayOfWeek = item.date.dayOfWeek ,Date = new DateTime(item.date.year, item.date.month, item.date.day), HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, LangEn = item.name[1].lang, TextEn = item.name[1].text };
                        tempList.Add(newCountry);
                    }
                }
                response.IsSuccess = true;
                response.List = tempList;
                return response;
            }       
             var queryAllRecords = _dbContext.Holidays.Where(x => (x.CountryCode == countryCode) && (x.Date.Year == int.Parse(year)) && (x.Region == region)).ToList();
             response.IsSuccess = true;
             response.List = queryAllRecords;
             return response;
        }
        public ResponseDto<DayStatus> CheckDayStatus(DateTime date, string country)
        {
            var hello = _dbContext.DayStatuses.FirstOrDefault(x => (x.CountryCode == country) && (x.Date == date));
            if (hello is null)
            {
                var result = _enricoApiService.SpecificDayStatus(date, country);
                _countryHolidayRepository.AddToDayStatusDatabase(date, result.Message, country);
                var newDay = new DayStatus() { CountryCode = country, TypeOfDay = result.Message, Date = date };
                return new ResponseDto<DayStatus>() { Message = newDay.TypeOfDay.ToString() };
            }
            return new ResponseDto<DayStatus>() { Message = hello.TypeOfDay.ToString() };
        }
        public ResponseDto<SupportedCountry> GetSupportedCountryList()
        {
            if (!_dbContext.Countries.Any())
            {
                var result = _enricoApiService.GetSupportedCountries();
                _countryHolidayRepository.AddToCountriesDatabase(result);
                return new ResponseDto<SupportedCountry>() { List = result };
            }
            var queryResult = _dbContext.Countries.Include(x=> x.region).AsEnumerable();
            return new ResponseDto<SupportedCountry>() {List = queryResult.SupportedCountryMap() };
        }
        public ResponseDto<int> GetConsecutive(string country, string year, string region)
        {
            var result = _dbContext.Holidays.Where(x => (x.CountryCode == country) && (x.Date.Year == int.Parse(year)) && (x.Region == region)).ToList();
            var dates = result.Select(x => x.Date).ToList();
            dates.Sort();
            var groups = new List<List<DateTime>>();
            var group1 = new List<DateTime>() { dates[0] };
            groups.Add(group1);       
            DateTime lastDate = dates[0];
            for (int i = 1; i < dates.Count; i++)
            {
                DateTime currDate = dates[i];
                TimeSpan timeDiff = currDate - lastDate;
                bool isNewGroup = timeDiff.Days > 1;
                if (isNewGroup)
                {
                    groups.Add(new List<DateTime>());
                }
                groups.Last().Add(currDate);
                lastDate = currDate;
            }
            var longestStreak = 0;
            var count = 0;
            foreach(var group in groups)
            {
                bool friday = false;
                bool saturday = false;
                bool monday = false;
                count = group.Count();
                foreach(var date in group)
                {                 
                    if(date.DayOfWeek == DayOfWeek.Friday)
                    {
                        friday = true;
                        count += 2;
                    }
                    else if(date.DayOfWeek == DayOfWeek.Monday && date.Date == group[0])
                    {
                        if(!friday)
                        {
                            monday = true;
                            count += 2;
                        }
                    }                 
                    else if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        saturday = true;
                        if (friday || monday)
                        {
                            count -= 1;
                        }
                    }
                    else if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (friday || monday)
                        {
                            count -= 1;
                        }
                    }
                    else
                    {
                        if(count > longestStreak)
                            longestStreak = count;                     
                    }                 
                }
            }
            if (count > longestStreak)
            {
                longestStreak = count;
            }
            return new ResponseDto<int>() { Message = longestStreak.ToString()};
        }
    }
}
