using System.ComponentModel.DataAnnotations;

namespace PublicHolidaysAssignment.Models
{
    public class Date
    { 
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int dayOfWeek { get; set; }
    }
}
