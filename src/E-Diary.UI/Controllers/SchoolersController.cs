using E_Diary.Core.Dto.Schooler;
using E_Diary.Core.Services;
using E_Diary.API.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Controllers
{
    [Route("api/schoolers")]
    [ApiController]
    public class SchoolersController : ControllerBase
    {
        private readonly ISchoolerService schoolerService;

        public SchoolersController(ISchoolerService subjectService)
        {
            this.schoolerService = subjectService;
        }

        [HttpPost]
        [ProducesCreated(typeof(int))]
        [ProducesBadRequest]
        [ProducesOk]
        public async Task<IActionResult> Create(SchoolerCreateRequest request)
        {
            var result = await schoolerService.CreateSchooler(request);
            return CreatedAtAction("Get", new { schoolerId = result }, result);
        }

        [HttpGet]
        [ProducesOk(typeof(IEnumerable<SchoolerResponse>))]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 100)
        {
            var result = await schoolerService.GetAllSchoolers(skip, take);
            return Ok(result);
        }

        [HttpGet("{schoolerId:int}")]
        [ProducesOk(typeof(SchoolerResponse))]
        [ProducesBadRequest]
        [ProducesNotFound]
        public async Task<IActionResult> Get(int schoolerId)
        {
            var result = await schoolerService.GetSchooler(schoolerId);
            return Ok(result);
        }

        [HttpPut("{schoolerId:int}")]
        [ProducesBadRequest]
        [ProducesNoContent]
        [ProducesNotFound]
        public async Task<IActionResult> Update(int schoolerId, SchoolerUpdateRequest request)
        {
            await schoolerService.UpdateSchooler(schoolerId, request);
            return NoContent();
        }

        [HttpDelete("{schoolerId:int}")]
        [ProducesNotFound]
        [ProducesNoContent]
        public async Task<IActionResult> Delete(int schoolerId)
        {
            await schoolerService.DeleteSchooler(schoolerId);
            return NoContent();
        }
    }
}
