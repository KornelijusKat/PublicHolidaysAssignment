namespace PublicHolidaysAssignment.Models
{
    public class Country
    {
        public Guid Id { get; set; }
        public string fullName { get; set; }
        public string countryCode { get; set; }
        public List<Region>? region { get; set; }
        public Country()
        {
            region = new List<Region>();
            Id = Guid.NewGuid();
        }
    }
}
