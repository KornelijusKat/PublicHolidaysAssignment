namespace PublicHolidaysAssignment.Models
{
    public class DayStatus
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string TypeOfDay { get; set; }
        public string CountryCode { get; set; }
        public DayStatus()
        {
            Id = Guid.NewGuid();
        }
    }
}
