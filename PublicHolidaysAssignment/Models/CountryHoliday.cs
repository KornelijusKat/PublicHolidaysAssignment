namespace PublicHolidaysAssignment.Models
{
    public class CountryHoliday
    {
        public Guid Id { get; set; }
        public string CountryCode { get; set; }
        public DateTime Date { get; set; }  
        public int DayOfWeek { get; set; }
        public string Lang { get; set; }
        public string Text { get; set; }
        public string? LangEn { get; set; }
        public string? TextEn { get; set; }
        public string? Region { get; set; }
        public string HolidayType { get; set; }
        public CountryHoliday()
        {
            Id = Guid.NewGuid();
        }
    }
}
