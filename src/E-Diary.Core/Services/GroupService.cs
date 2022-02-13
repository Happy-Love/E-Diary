using E_Diary.Core.Dto.Group;
using E_Diary.Core.Exceptions;
using E_Diary.Core.Specifications.Groups;
using E_Diary.Domain.Entities;
using E_Diary.Domain.Entities.Interfaces;

namespace E_Diary.Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> groupRepository;

        public GroupService(IRepository<Group> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<int> CreateGroup(GroupCreateRequest request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            var entity = new Group()
            {
                Name = request.Name,
            };

            await groupRepository.Add(entity);

            var result = entity.Id;
            return result;

        }

        public async Task DeleteGroup(int groupId)
        {
            if (groupId < 0)
                throw new ArgumentOutOfRangeException(nameof(groupId));

            var entity = await groupRepository.GetById(groupId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            await groupRepository.Delete(entity);
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroups(int skip = 0, int take = 100)
        {
            var specification = new GroupSpecification();

            var groups = await groupRepository.List(specification, skip, take);
            var result = groups
                .Select(group => new GroupResponse() { Id = group.Id, Name = group.Name })
                .ToArray();

            return result;
        }

        public async Task<GroupResponse> GetGroup(int groupId)
        {
            if (groupId < 0)
                throw new ArgumentOutOfRangeException(nameof(groupId));

            var entity = await groupRepository.GetById(groupId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            var result = new GroupResponse()
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return result;
        }

        public async Task UpdateGroup(int groupId, GroupUpdateRequest request)
        {
            if (groupId < 0)
                throw new ArgumentOutOfRangeException(nameof(groupId));

            ArgumentNullException.ThrowIfNull(nameof(request));

            if (groupId != request.Id)
                throw new ValidationException("group identifier from route is not equal group identifier in body");

            var entity = await groupRepository.GetById(groupId);
            if (entity == null)
                throw new ResourceNotFoundException(nameof(entity));

            entity.Name = request.Name;

            await groupRepository.Update(entity);
        }
    }
}
