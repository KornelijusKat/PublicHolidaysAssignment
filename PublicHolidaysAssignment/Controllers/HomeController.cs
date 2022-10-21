using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicHolidaysAssignment.EnricoApi;
using PublicHolidaysAssignment.Models;
using PublicHolidaysAssignment.PublicHolidayServices;

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
        [HttpGet("Hello")]
        public IActionResult Index()
        {
            var Enrico = new EnricoApi.EnricoApi(Clienta);
            var listas = _publicHolidayService.GetSupportedCountryList();
            return Ok(listas);
        }
        [HttpGet("GroupedListOfHolidays")]
        public IActionResult Index1(string year, string country, string? region)
        {
            var result = _publicHolidayService.GetPublicHolidays(year, country, region);
            return Ok(result);
        }
        [HttpGet("IsDayHoliday")]
        public IActionResult Index2(string year, string country)
        {
            var result = _publicHolidayService.CheckDayStatus(year, country);
            return Ok(result);
        }
    }
}
