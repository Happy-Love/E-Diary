namespace E_Diary.Core.Dto.Mark
{
    public class SchoolerMarkUpdateRequest
    {
        public int SubjectId { get; set; }
        public int SchoolerId { get; set; }
        public int? Score { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
