using E_Diary.Core.Dto.Subject;
using E_Diary.Core.Exceptions;
using E_Diary.Core.Specifications.Subjects;
using E_Diary.Domain.Entities;
using E_Diary.Domain.Entities.Interfaces;

namespace E_Diary.Core.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IRepository<Subject> subjectRepository;

        public SubjectService(IRepository<Subject> subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public async Task<int> CreateSubject(SubjectCreateRequest request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var subject = new Subject()
            {
                Name = request?.Name ?? throw new ValidationException(nameof(request.Name)),
            };

            await subjectRepository.Add(subject);

            return subject.Id;
        }

        public async Task DeleteSubject(int subjectId)
        {
            if (subjectId < 0)
                throw new ArgumentOutOfRangeException(nameof(subjectId));

            var entity = await subjectRepository.GetById(subjectId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            await subjectRepository.Delete(entity);
        }

        public async Task<IEnumerable<SubjectResponse>> GetAllSubjects(int skip = 0, int take = 100)
        {
            var specification = new SubjectSpecification();

            var subjects = await subjectRepository.List(specification, skip, take);

            var result = subjects
                .Select(t => new SubjectResponse()
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return result;
        }

        public async Task<SubjectResponse> GetSubject(int subjectId)
        {
            if (subjectId < 0)
                throw new ArgumentOutOfRangeException(nameof(subjectId));

            var entity = await subjectRepository.GetById(subjectId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            var result = new SubjectResponse()
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return result;
        }

        public async Task UpdateSubject(int subjectId, SubjectUpdateRequest request)
        {
            if (subjectId < 0)
                throw new ArgumentOutOfRangeException(nameof(subjectId));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var entity = await subjectRepository.GetById(subjectId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            entity.Name = request.Name;

            await subjectRepository.Update(entity);
        }
    }
}
