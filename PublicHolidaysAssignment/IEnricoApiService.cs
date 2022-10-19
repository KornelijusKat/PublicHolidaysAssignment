namespace PublicHolidaysAssignment
{
    public interface IEnricoApiService
    {
        Task<string> GetHolidaysOfGivenCountryAndYear(string year, string country);
        string SpecificDayStatus(string date, string country);
    }
}
