namespace PublicHolidaysAssignment.Models
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string CountryCode { get; set; }
        public Region()
        {
            Id = Guid.NewGuid();
        }
    }
}
