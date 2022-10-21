using AutoMapper;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.Extensions
{
    public static class Mappers
    {
        public static IEnumerable<SupportedCountry> maps(this IEnumerable<Country> List)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<List<SupportedCountry>, List<Country>>());
            var mapper = new Mapper(config);
            var supportedCountry = mapper.Map<List<SupportedCountry>>(List);
            return supportedCountry;

        }
    }
}
