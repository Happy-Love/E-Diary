using E_Diary.Domain;
using E_Diary.Domain.Entities;

namespace E_Diary.Core.Specifications.Groups
{
    public class GroupSpecification : BaseSpecification<Group>
    {
        public GroupSpecification()
        {
            Includes.Add(t => t.Schoolers);
        }
    }
}
