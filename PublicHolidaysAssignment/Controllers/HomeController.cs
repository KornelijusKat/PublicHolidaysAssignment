using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.PublicHolidayServices;
using PublicHolidaysAssignment.RequestModels;

namespace PublicHolidaysAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class HomeController : Controller
    {
        public HttpClient Clienta = new HttpClient();
        private readonly IEnricoApiService _enricoApiService;
        private readonly IPublicHolidayService _publicHolidayService;
        public HomeController(IEnricoApiService enricoApiService, IPublicHolidayService publicHolidayService)
        {
            _enricoApiService = enricoApiService;
            _publicHolidayService = publicHolidayService;
        }
        [HttpGet("GetCountryList")]
        public IActionResult Index()
        {
            var Enrico = new EnricoApi.EnricoApi(Clienta);
            var listas = _publicHolidayService.GetSupportedCountryList();
            if (!listas.IsSuccess)
                return BadRequest(listas.Message);
            return Ok(listas);
        }
        [HttpPost("GroupedListOfHolidays")]
        public IActionResult Index1([FromQuery]YearRequest syear, string country, string? region)
        {
            var result = _publicHolidayService.GetPublicHolidays(syear.Year, country, region);
            if(!result.IsSuccess)
                return BadRequest(result.Message);
            return Ok(result);   
        }
        [HttpGet("IsDayHoliday")]
        public IActionResult Index2(DateTime year, string country)
        {
            var result = _publicHolidayService.CheckDayStatus(year, country);
            return Ok(result);
        }
        [HttpGet("LongestDayOffSequence")]
        public IActionResult Index3(string country,string year, string? region)
        {
            var result = _publicHolidayService.GetConsecutive(country,year,region);
            return Ok(result);
        }
    }
}
