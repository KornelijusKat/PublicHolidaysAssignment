using System.ComponentModel.DataAnnotations;

namespace PublicHolidaysAssignment.RequestModels
{
    public class YearRequest
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Please input valid year")]
        public string Year { get; set; }
    }
}
