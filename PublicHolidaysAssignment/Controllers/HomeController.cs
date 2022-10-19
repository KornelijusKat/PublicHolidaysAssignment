using Microsoft.AspNetCore.Mvc;

namespace PublicHolidaysAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public HttpClient Clienta = new HttpClient();
        private readonly IEnricoApiService _enricoApiService;
        public HomeController(IEnricoApiService enricoApiService)
        {
            _enricoApiService = enricoApiService;
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
            var result = _enricoApiService.GetHolidaysOfGivenCountryAndYear(year,country);
            return Ok(result.Result);
        }
        [HttpGet("IsDayHoliday")]
        public IActionResult Index2(string year, string country)
        {
            var result = _enricoApiService.SpecificDayStatus(year.ToString(), country);
            return Ok(result);
        }
    }
}
