using AutoMapper;
using AutoMapper.Internal;
using PublicHolidaysAssignment.Models;

namespace PublicHolidaysAssignment.Extensions
{
    public static class Mappers
    {
        public static IEnumerable<SupportedCountry> SupportedCountryMap(this IEnumerable<Country> List)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Country, SupportedCountry>().ForMember(d => d.region, s => s.MapFrom(e => e.region.Select(x=> x.Name))));
            var mapper = new Mapper(config);
            var supportedCountry = mapper.Map<List<SupportedCountry>>(List);
            return supportedCountry;
        }
    }
}
