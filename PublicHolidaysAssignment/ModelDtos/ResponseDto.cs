namespace PublicHolidaysAssignment.ModelDtos
{
    public class ResponseDto<t>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<t> List { get; set; }
        public ResponseDto()
        {
            IEnumerable<t> list = new List<t>();
            IsSuccess = true;
        }   
    }
}
