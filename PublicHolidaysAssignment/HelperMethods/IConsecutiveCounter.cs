namespace PublicHolidaysAssignment.HelperMethods
{
    public interface IConsecutiveCounter
    {
        int CountMaxConsecutiveDays(List<List<DateTime>> groups);
        List<List<DateTime>> SeparateByConsecutiveDays(List<DateTime> dates);
    }
}
