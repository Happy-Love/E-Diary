using E_Diary.Domain;
using E_Diary.Domain.Entities;

namespace E_Diary.Core.Specifications.Subjects
{
    public class SubjectSpecification : BaseSpecification<Subject>
    {
        public SubjectSpecification()
        {
            Includes.Add(t => t.SchoolerMarks);
        }
    }
}
