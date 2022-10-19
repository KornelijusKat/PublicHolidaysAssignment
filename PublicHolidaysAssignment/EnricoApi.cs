using static System.Net.WebRequestMethods;

namespace PublicHolidaysAssignment
{
    public class EnricoApi
    {
        private readonly HttpClient _http;
        public EnricoApi(HttpClient http)
        {
            _http = http;
        }
        public async Task<string> HttpClientExtension(string uriEnding)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://kayaposoft.com/enrico/json/v2.0?action=" + uriEnding)
                };
                using var response = await _http.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
