namespace E_Diary.Core.Dto.Schooler
{
    public class SchoolerUpdateRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public int GroupId { get; set; }
    }
}
