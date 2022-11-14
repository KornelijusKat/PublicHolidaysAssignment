using System.Text.RegularExpressions;

namespace PublicHolidaysAssignment.HelperMethods
{
    public class ConsecutiveCounter : IConsecutiveCounter
    {
        public int CountMaxConsecutiveDays(List<List<DateTime>> groups)
        {
            var longestStreak = 0;
            var count = 0;
            foreach (var group in groups)
            {
                bool friday = false;
                bool saturday = false;
                bool monday = false;
                count = group.Count();
                foreach (var date in group)
                {
                    if (date.DayOfWeek == DayOfWeek.Friday)
                    {
                        friday = true;
                        count += 2;
                    }
                    else if (date.DayOfWeek == DayOfWeek.Monday && date.Date == group[0])
                    {
                        if (!friday)
                        {
                            monday = true;
                            count += 2;
                        }
                    }
                    else if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        saturday = true;
                        if (friday || monday)
                        {
                            count -= 1;
                        }
                    }
                    else if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        if (friday || monday)
                        {
                            count -= 1;
                        }
                    }
                    else
                    {
                        if (count > longestStreak)
                            longestStreak = count;
                    }
                }
            }
            if (count > longestStreak)
            {
                longestStreak = count;
            }
            return longestStreak;
        }
        public List<List<DateTime>> SeparateByConsecutiveDays(List<DateTime> dates)
        {
            dates.Sort();
            var groups = new List<List<DateTime>>();
            var group1 = new List<DateTime>() { dates[0] };
            groups.Add(group1);
            DateTime lastDate = dates[0];
            for (int i = 1; i < dates.Count; i++)
            {
                DateTime currDate = dates[i];
                TimeSpan timeDiff = currDate - lastDate;
                bool isNewGroup = timeDiff.Days > 1;
                if (isNewGroup)
                {
                    groups.Add(new List<DateTime>());
                }
                groups.Last().Add(currDate);
                lastDate = currDate;
            }
            return groups;
        }
    }
}
