using E_Diary.Domain;
using E_Diary.Domain.Entities;

namespace E_Diary.Core.Specifications.Schoolers
{
    public class SchoolerSpecification : BaseSpecification<Schooler>
    {
        public SchoolerSpecification()
        {
            Includes.Add(t => t.Group);
        }
    }
}
