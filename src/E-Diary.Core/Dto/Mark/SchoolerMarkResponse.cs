using E_Diary.Core.Dto.Subject;

namespace E_Diary.Core.Dto.Mark
{
    public class SchoolerMarkResponse
    {
        public int? Score { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public SubjectResponse? Subject { get; set; }
    }
}
