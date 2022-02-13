using E_Diary.Core.Dto.Group;

namespace E_Diary.Core.Dto.Schooler
{
    public class SchoolerResponse
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public GroupResponse? Group { get; set; }
    }
}
