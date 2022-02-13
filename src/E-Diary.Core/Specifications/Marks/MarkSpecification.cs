using E_Diary.Domain;
using E_Diary.Domain.Entities;

namespace E_Diary.Core.Marks
{
    public class MarkSpecification : BaseSpecification<SchoolerMark>
    {
        public MarkSpecification(int schoolerId)
        {
            Criteria = t => t.SchoolerId == schoolerId;

            Includes.Add(t => t.Subject);
            Includes.Add(t => t.Schooler);
        }
    }
}
