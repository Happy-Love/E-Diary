using E_Diary.Core.Dto.Mark;
using E_Diary.Core.Dto.Operation;
using E_Diary.Core.Dto.Subject;
using E_Diary.Core.Exceptions;
using E_Diary.Core.IntegrationServices;
using E_Diary.Core.Marks;
using E_Diary.Domain.Entities;
using E_Diary.Domain.Entities.Interfaces;

namespace E_Diary.Core.Services
{
    public class SchoolerMarkService : ISchoolerMarkService
    {
        private readonly IRepository<Schooler> schoolerRepository;
        private readonly IRepository<Subject> subjectRepository;
        private readonly IRepository<SchoolerMark> markRepository;

        private readonly IGCloudBillingService gCloudBillingService;

        public SchoolerMarkService(IRepository<Schooler> schoolerRepository,
            IRepository<Subject> subjectRepository,
            IRepository<SchoolerMark> markRepository,
            IGCloudBillingService gCloudBillingService
            )
        {
            this.schoolerRepository = schoolerRepository;
            this.subjectRepository = subjectRepository;
            this.markRepository = markRepository;
            this.gCloudBillingService = gCloudBillingService;
        }

        public async Task<int> CreateSchoolerMark(int schoolerId, SchoolerMarkCreateRequest request)
        {
            if (schoolerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(schoolerId));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var schooler = await schoolerRepository.GetById(schoolerId);
            if (schooler == null)
                throw new ResourceNotFoundException(nameof(schooler));

            var subject = await subjectRepository.GetById(request.SubjectId);
            if (subject == null)
                throw new ValidationException(nameof(subject));

            var operationResult = await gCloudBillingService.TryProcessingRequest(new OperationRequest()
            {
                OperationId = Guid.NewGuid(),
                Mark = request.Score
            });

            if (!operationResult.IsAllowed)
            {
                throw new OperationNotAllowedException("Mark has invalid values, should be from 0 to 10");
            }

            var entity = new SchoolerMark()
            {
                CreatedAt = request.CreatedAt,
                Score = request.Score,
                SchoolerId = request.SchoolerId,
                SubjectId = request.SubjectId,
                Schooler = schooler,
                Subject = subject,
            };

            await markRepository.Add(entity);
            return entity.Id;
        }

        public async Task DeleteSchoolerMark(int schoolerId, int markId)
        {
            if (schoolerId < 0)
                throw new ArgumentOutOfRangeException(nameof(schoolerId));

            if (markId < 0)
                throw new ArgumentOutOfRangeException(nameof(markId));

            var schooler = await schoolerRepository.GetById(schoolerId);
            if (schooler == null)
                throw new ResourceNotFoundException(nameof(schooler));

            var mark = await markRepository.GetById(markId);
            if (mark == null)
                throw new ResourceNotFoundException(nameof(mark));

            await markRepository.Delete(mark);
        }

        public async Task<IEnumerable<SchoolerMarkResponse>> GetAllSchoolerMarks(int schoolerId, int skip = 0, int take = 100)
        {
            if (schoolerId < 0)
                throw new ArgumentOutOfRangeException(nameof(schoolerId));

            var specification = new MarkSpecification(schoolerId);
            var marks = await markRepository.List(specification, skip, take);

            var result = marks
                .Select(t => new SchoolerMarkResponse()
                {
                    CreatedAt = t.CreatedAt,
                    Score = t.Score,
                    Subject = new SubjectResponse()
                    {
                        Id = t.SubjectId,
                        Name = t.Subject?.Name ?? "Unknown"
                    }
                });

            return result;
        }

        public async Task<SchoolerMarkResponse> GetSchoolerMark(int schoolerId, int markId)
        {
            if (markId < 0)
                throw new ArgumentOutOfRangeException(nameof(markId));

            var entity = await markRepository.GetById(markId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            if (entity.SchoolerId != schoolerId)
                throw new ValidationException(nameof(schoolerId));

            var result = new SchoolerMarkResponse()
            {
                CreatedAt = entity.CreatedAt,
                Score = entity.Score,
                Subject = new SubjectResponse()
                {
                    Id = entity.SubjectId,
                    Name = entity.Subject?.Name ?? "Unknown"
                }
            };

            return result;
        }

        public async Task UpdateSchoolerMark(int schoolerId, int markId, SchoolerMarkUpdateRequest request)
        {
            if (markId < 0)
                throw new ArgumentOutOfRangeException(nameof(markId));

            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var entity = await markRepository.GetById(markId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            var subject = await subjectRepository.GetById(request.SubjectId);
            if (subject == null)
                throw new ValidationException(nameof(subject));

            var schooler = await schoolerRepository.GetById(request.SchoolerId);
            if (schooler == null)
                throw new ValidationException(nameof(schooler));

            entity.Score = request.Score;
            entity.CreatedAt = request.CreatedAt;
            entity.SchoolerId = entity.SchoolerId;
            entity.SubjectId = entity.SubjectId;

            await markRepository.Update(entity);
        }
    }
}
