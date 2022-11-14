using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.Extensions;
using PublicHolidaysAssignment.HelperMethods;
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
        private readonly IConsecutiveCounter _consecutiveCounter;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly HolidayDbContext _dbContext;
        public PublicHolidayService(ICountryHolidayRepository countryHolidayRepository, IEnricoApiService enricoApiService, HolidayDbContext dbContext, IConsecutiveCounter consecutiveCounter, IJsonDeserializer jsonDeserializer)
        {
            _enricoApiService = enricoApiService;
            _countryHolidayRepository = countryHolidayRepository;
            _dbContext = dbContext;
            _consecutiveCounter = consecutiveCounter;
            _jsonDeserializer = jsonDeserializer;
        }
        public ResponseDto<CountryHoliday> GetPublicHolidays(string year, string countryCode, string region)
        {
            var response = new ResponseDto<CountryHoliday>();
            var countriesWithRegions = new string[] { "nzl", "aus", "can", "usa", "us", "deu", "de", "gbr", "gb" };
            if (!countriesWithRegions.Contains(countryCode))
                {
                region = null;
                }
            var recordsExists = _countryHolidayRepository.QueryIfCountryHolidayExists(countryCode, year, region);
            if(recordsExists is null)
            {
                var result = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year, countryCode, region);
                if(!result.IsSuccess)
                {
                    return result;
                }
                var tempList = _jsonDeserializer.CountryHolidayDeserializer(countryCode, result);
                response.IsSuccess = true;
                response.List = tempList;
                return response;
            }
            var queryAllRecords = _countryHolidayRepository.GetOrderedList(countryCode,year, region);
            response.List = queryAllRecords;
            return response;
        }
        public ResponseDto<DayStatus> CheckDayStatus(DateTime date, string country)
        {
            var hello = _countryHolidayRepository.QueryDayRecord(country,date);
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
            if (!_countryHolidayRepository.QueryIfAnyRecordExists())
            {
                var result = _enricoApiService.GetSupportedCountries();
                _countryHolidayRepository.AddToCountriesDatabase(result);
                return new ResponseDto<SupportedCountry>() { List = result };
            }
            var queryResult = _countryHolidayRepository.GetSupportedCountriesAndTheirRegions();
            return new ResponseDto<SupportedCountry>() {List = queryResult.SupportedCountryMap() };
        }
        public ResponseDto<int> GetConsecutive(string country, string year, string region)
        {
            var recordsExists = _countryHolidayRepository.QueryIfCountryHolidayExists(country, year, region);
            var result = new List<CountryHoliday>();
            if (recordsExists is null)
            {
                var apiResponse = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year,country, region);
                result = (List<CountryHoliday>)_jsonDeserializer.CountryHolidayDeserializer(country,apiResponse);
            }
            else
            {
                result = _countryHolidayRepository.GetOrderedList(country,year,region).ToList();
            }
            var dates = result.Select(x => x.Date).ToList();
            var groups = _consecutiveCounter.SeparateByConsecutiveDays(dates);
            var longestStreak = _consecutiveCounter.CountMaxConsecutiveDays(groups);        
            return new ResponseDto<int>() { Message = longestStreak.ToString()};
        }
    }
}
