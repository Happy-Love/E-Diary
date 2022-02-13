using E_Diary.Core.Dto.Mark;
using E_Diary.Core.Services;
using E_Diary.API.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Controllers
{
    [ApiController]
    [Route("api/schoolers/{schoolerId:int}/marks")]
    public class SchoolerMarksController : ControllerBase
    {
        private readonly ISchoolerMarkService markService;

        public SchoolerMarksController(ISchoolerMarkService markService)
        {
            this.markService = markService;
        }

        [HttpPost]
        [ProducesCreated(typeof(int))]
        [ProducesBadRequest]
        [ProducesNotFound]
        [ProducesOk]
        public async Task<IActionResult> Create([FromRoute] int schoolerId, SchoolerMarkCreateRequest request)
        {
            var result = await markService.CreateSchoolerMark(schoolerId,request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesOk(typeof(IEnumerable<SchoolerMarkResponse>))]
        [ProducesBadRequest]
        [ProducesNotFound]
        public async Task<IActionResult> GetAll(
            [FromRoute] int schoolerId,
            int skip = 0, int take = 100)
        {
            var result = await markService.GetAllSchoolerMarks(schoolerId, skip, take);
            return Ok(result);
        }

        [HttpGet("{markId:int}")]
        [ProducesOk(typeof(SchoolerMarkResponse))]
        [ProducesBadRequest]
        [ProducesNotFound]
        public async Task<IActionResult> Get(
            [FromRoute] int schoolerId,
            [FromRoute] int markId)
        {
            var result = await markService.GetSchoolerMark(schoolerId, markId);
            return Ok(result);
        }

        [HttpPut("{markId:int}")]
        [ProducesBadRequest]
        [ProducesNoContent]
        [ProducesNotFound]
        public async Task<IActionResult> Update(
            [FromRoute] int schoolerId,
            [FromRoute] int markId,
            SchoolerMarkUpdateRequest request)
        {
            await markService.UpdateSchoolerMark(schoolerId, markId, request);
            return NoContent();
        }

        [HttpDelete("{markId:int}")]
        [ProducesNotFound]
        [ProducesNoContent]
        public async Task<IActionResult> Delete(
            [FromRoute] int schoolerId,
            [FromRoute] int markId)
        {
            await markService.DeleteSchoolerMark(schoolerId, markId);
            return NoContent();
        }
    }
}
