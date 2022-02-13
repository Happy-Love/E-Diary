using E_Diary.Core.Dto.Mark;

namespace E_Diary.Core.Services
{
    public interface ISchoolerMarkService
    {
        Task<int> CreateSchoolerMark(int schoolerId, SchoolerMarkCreateRequest request);
        Task UpdateSchoolerMark(int schoolerId, int markId, SchoolerMarkUpdateRequest request);
        Task<SchoolerMarkResponse> GetSchoolerMark(int schoolerId, int markId);
        Task<IEnumerable<SchoolerMarkResponse>> GetAllSchoolerMarks(int schoolerId, int skip = 0, int take = 100);
        Task DeleteSchoolerMark(int schoolerId, int markId);
    }
}
