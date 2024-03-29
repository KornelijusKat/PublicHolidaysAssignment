﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PublicHolidaysAssignment.Models;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace PublicHolidaysAssignment.Repository
{
    public class CountryHolidayRepository :ICountryHolidayRepository
    {
        private readonly HolidayDbContext _context; 
        public CountryHolidayRepository(HolidayDbContext context)
        {
            _context = context;
        }
        public bool AddToDatabase(string body,string country, string region)
        {
           if(body.Contains("error"))
            {
                return false;
            }
                CultureInfo provider = CultureInfo.InvariantCulture;
            var countriesWithRegions = new string[] { "nzl", "aus", "can", "usa", "us", "deu", "de", "gbr", "gb" };
            var deserialized = JsonConvert.DeserializeObject<List<Root>>(body);
                if (countriesWithRegions.Contains(country))
                {
                    foreach (var item in deserialized)
                    {
                        var newCountry = new CountryHoliday() { CountryCode = country, DayOfWeek = item.date.dayOfWeek, Date = new DateTime(item.date.year, item.date.month, item.date.day), HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, Region = region };
                        _context.Holidays.Add(newCountry);
                    }
                }
                else
                {
                    foreach (var item in deserialized)
                    {
                        var newCountry = new CountryHoliday() { CountryCode = country, DayOfWeek = item.date.dayOfWeek, Date = new DateTime(item.date.year, item.date.month, item.date.day), HolidayType = item.holidayType, Lang = item.name[0].lang, Text = item.name[0].text, LangEn = item.name[1].lang, TextEn = item.name[1].text };
                        _context.Holidays.Add(newCountry);
                    }
                }
            _context.SaveChanges();
            return true;
        }
        public void AddToDayStatusDatabase(DateTime date, string body, string country)
        {
            var newDay = new DayStatus() { CountryCode = country, TypeOfDay = body, Date = date };
            _context.DayStatuses.Add(newDay);
            _context.SaveChanges();
        }
        public void AddToCountriesDatabase(List<SupportedCountry> supportedCountries)
        {
            foreach(var item in supportedCountries)
            {
                var newCountry = new Country() { countryCode = item.countryCode, fullName = item.fullName };
                if (item.region != null)
                {
                    foreach (var region in item.region)
                    {
                        var newRegion = new Region() { CountryCode = item.countryCode, Name = region };
                        newCountry.region.Add(newRegion);
                    }
                }
                _context.Countries.Add(newCountry);
            }
            _context.SaveChanges();
        }
        public DayStatus QueryDayRecord(string country, DateTime date)
        {
            return _context.DayStatuses.FirstOrDefault(x => (x.CountryCode == country) && (x.Date == date));
        }
        public bool QueryIfAnyRecordExists()
        {
            return _context.Countries.Any();
        }
        public IEnumerable<CountryHoliday> GetOrderedList(string countryCode, string year, string region)
        {
            return _context.Holidays.Where(x => (x.CountryCode == countryCode) && (x.Date.Year == int.Parse(year)) && (x.Region == region)).ToList().OrderBy(x => x.Date);
        }
        public CountryHoliday QueryIfCountryHolidayExists(string country, string year, string region)
        {
            return _context.Holidays.FirstOrDefault(x => (x.CountryCode == country) && (x.Date.Year == int.Parse(year)) && (x.Region == region));
        }
        public IEnumerable<Country> GetSupportedCountriesAndTheirRegions()
        {
            return _context.Countries.Include(x => x.region).AsEnumerable(); 
        }
    }
}
