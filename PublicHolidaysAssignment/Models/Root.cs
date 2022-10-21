using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PublicHolidaysAssignment.Models
{
    public class Root
    {
        public Date date { get; set; }
        public List<Name> name { get; set; }
        public string holidayType { get; set; }
    }
}
