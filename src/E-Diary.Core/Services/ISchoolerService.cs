using E_Diary.Core.Dto.Schooler;

namespace E_Diary.Core.Services
{
    public interface ISchoolerService
    {
        Task<int> CreateSchooler(SchoolerCreateRequest request);
        Task UpdateSchooler(int schoolerId, SchoolerUpdateRequest request);
        Task<SchoolerResponse> GetSchooler(int schoolerId);
        Task<IEnumerable<SchoolerResponse>> GetAllSchoolers(int skip = 0, int take = 100);
        Task DeleteSchooler(int schoolerId);
    }
}
