using E_Diary.Core.Dto.Operation;

namespace E_Diary.Core.IntegrationServices
{
    public interface IGCloudBillingService
    {
        Task<OperationResponse?> TryProcessingRequest(OperationRequest request);
    }
}
