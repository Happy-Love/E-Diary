using E_Diary.Core.Dto.Group;


namespace E_Diary.Core.Services
{
    public interface IGroupService
    {
        Task<int> CreateGroup(GroupCreateRequest request);
        Task UpdateGroup(int groupId, GroupUpdateRequest request);
        Task<GroupResponse> GetGroup(int groupId);
        Task<IEnumerable<GroupResponse>> GetAllGroups(int skip = 0, int take = 100);
        Task DeleteGroup(int groupId);
    }
}
