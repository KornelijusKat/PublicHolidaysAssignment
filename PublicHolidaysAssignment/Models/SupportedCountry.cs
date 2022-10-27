namespace PublicHolidaysAssignment.Models
{
    public class SupportedCountry
    {
        public string fullName { get; set; }
        public string countryCode { get; set; }
        public List<string>? region { get; set; }
        public SupportedCountry()
        {
            region = new List<string>();
        }
    }
}
