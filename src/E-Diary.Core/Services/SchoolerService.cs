using E_Diary.Core.Dto.Group;
using E_Diary.Core.Dto.Schooler;
using E_Diary.Core.Exceptions;
using E_Diary.Core.Specifications.Schoolers;
using E_Diary.Domain.Entities;
using E_Diary.Domain.Entities.Interfaces;

namespace E_Diary.Core.Services
{
    public class SchoolerService : ISchoolerService
    {
        private readonly IRepository<Schooler> schoolerRepository;
        private readonly IRepository<Group> groupRepository;

        public SchoolerService(IRepository<Schooler> schoolerRepository,
            IRepository<Group> groupRepository)
        {
            this.schoolerRepository = schoolerRepository;
            this.groupRepository = groupRepository;
        }

        public async Task<int> CreateSchooler(SchoolerCreateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var group = await groupRepository.GetById(request.GroupId);
            if (group == null)
                throw new ValidationException(nameof(group));

            var entity = new Schooler()
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                GroupId = request.GroupId,
            };

            await schoolerRepository.Add(entity);

            return entity.Id;
        }

        public async Task DeleteSchooler(int schoolerId)
        {
            if (schoolerId < 0)
                throw new ArgumentOutOfRangeException(nameof(schoolerId));

            var entity = await schoolerRepository.GetById(schoolerId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            await schoolerRepository.Delete(entity);
        }

        public async Task<IEnumerable<SchoolerResponse>> GetAllSchoolers(int skip = 0, int take = 100)
        {
            var specification = new SchoolerSpecification();

            var schoolers = await schoolerRepository.List(specification, skip, take);

            var result = schoolers
                .Select(t => new SchoolerResponse()
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    MiddleName = t.MiddleName,
                    LastName = t.LastName,
                    Group = new GroupResponse()
                    {
                        Id = t.GroupId,
                        Name = t.Group?.Name ?? "Unknown"
                    }
                }).ToArray();

            return result;
        }

        public async Task<SchoolerResponse> GetSchooler(int schoolerId)
        {
            if (schoolerId < 0)
                throw new ArgumentOutOfRangeException(nameof(schoolerId));

            var entity = await schoolerRepository.GetById(schoolerId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            var group = await groupRepository.GetById(entity.GroupId);
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            var groupResponse = new GroupResponse()
            {
                Id = group.Id,
                Name = group.Name
            };

            var result = new SchoolerResponse()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
                Group = groupResponse
            };

            return result;
        }

        public async Task UpdateSchooler(int schoolerId, SchoolerUpdateRequest request)
        {
            if (schoolerId < 0)
                throw new ArgumentOutOfRangeException(nameof(schoolerId));

            ArgumentNullException.ThrowIfNull(nameof(request));

            var entity = await schoolerRepository.GetById(schoolerId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            var group = await groupRepository.GetById(request.GroupId);
            if (group == null)
                throw new ValidationException(nameof(request.GroupId));

            entity.FirstName = request.FirstName;
            entity.MiddleName = request.MiddleName;
            entity.LastName = request.LastName;
            entity.GroupId = request.GroupId;

            await schoolerRepository.Update(entity);
        }
    }
}
