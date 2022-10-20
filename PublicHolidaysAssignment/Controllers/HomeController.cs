using Microsoft.AspNetCore.Mvc;
using PublicHolidaysAssignment.PublicHolidayServices;

namespace PublicHolidaysAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var Enrico = new EnricoApi(Clienta);
            var result = Enrico.HttpClientExtension("getSupportedCountries");
            return Ok(result.Result);
        }
        [HttpGet("GroupedListOfHolidays")]
        public IActionResult Index1(string year, string country)
        {
            //var result = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year,country);
            var result = _publicHolidayService.GetPublicHolidays(year, country);
            return Ok(result);
        }
        [HttpGet("IsDayHoliday")]
        public IActionResult Index2(string year, string country)
        {
            var result = _enricoApiService.SpecificDayStatus(year.ToString(), country);
            return Ok(result);
        }
    }
}
