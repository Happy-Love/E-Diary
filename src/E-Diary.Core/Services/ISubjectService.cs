using E_Diary.Core.Dto.Subject;

namespace E_Diary.Core.Services
{
    public interface ISubjectService
    {
        Task<int> CreateSubject(SubjectCreateRequest request);
        Task UpdateSubject(int subjectId, SubjectUpdateRequest request);
        Task<SubjectResponse> GetSubject(int subjectId);
        Task<IEnumerable<SubjectResponse>> GetAllSubjects(int skip = 0, int take = 100);
        Task DeleteSubject(int subjectId);
    }
}
