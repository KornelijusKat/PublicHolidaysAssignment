namespace PublicHolidaysAssignment.PublicHolidayServices
{
    public interface IPublicHolidayService
    {
        string GetPublicHolidays(string year, string countryCode);
    }
}
