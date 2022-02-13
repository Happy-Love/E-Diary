using E_Diary.Core.Dto.Subject;
using E_Diary.Core.Services;
using E_Diary.API.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpPost]
        [ProducesCreated(typeof(int))]
        [ProducesBadRequest]
        [ProducesOk]
        public async Task<IActionResult> Create(SubjectCreateRequest request)
        {
            var result = await subjectService.CreateSubject(request);
            return CreatedAtAction("Get", new { subjectId = result }, result);
        }

        [HttpGet]
        [ProducesOk(typeof(IEnumerable<SubjectResponse>))]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 100)
        {
            var result = await subjectService.GetAllSubjects(skip, take);
            return Ok(result);
        }

        [HttpGet("{subjectId:int}")]
        [ProducesOk(typeof(SubjectResponse))]
        [ProducesBadRequest]
        [ProducesNotFound]
        public async Task<IActionResult> Get(int subjectId)
        {
            var result = await subjectService.GetSubject(subjectId);
            return Ok(result);
        }

        [HttpPut("{subjectId:int}")]
        [ProducesBadRequest]
        [ProducesNoContent]
        [ProducesNotFound]
        public async Task<IActionResult> Update(int subjectId, SubjectUpdateRequest request)
        {
            await subjectService.UpdateSubject(subjectId, request);
            return NoContent();
        }

        [HttpDelete("{subjectId:int}")]
        [ProducesNotFound]
        [ProducesNoContent]
        public async Task<IActionResult> Delete(int subjectId)
        {
            await subjectService.DeleteSubject(subjectId);
            return NoContent();
        }
    }
}
