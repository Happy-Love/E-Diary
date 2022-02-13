using E_Diary.Core.Dto.Group;
using E_Diary.Core.Services;
using E_Diary.API.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpPost]
        [ProducesCreated(typeof(int))]
        [ProducesBadRequest]
        [ProducesOk]
        public async Task<IActionResult> Create(GroupCreateRequest request)
        {
            var result = await groupService.CreateGroup(request);
            return CreatedAtAction("Get", new { groupId = result }, result);
        }

        [HttpGet]
        [ProducesOk(typeof(IEnumerable<GroupResponse>))]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 100)
        {
            var result = await groupService.GetAllGroups(skip, take);
            return Ok(result);
        }

        [HttpGet("{groupId:int}")]
        [ProducesOk(typeof(GroupResponse))]
        [ProducesBadRequest]
        [ProducesNotFound]
        public async Task<IActionResult> Get(int groupId)
        {
            var result = await groupService.GetGroup(groupId);
            return Ok(result);
        }

        [HttpPut("{groupId:int}")]
        [ProducesBadRequest]
        [ProducesNoContent]
        [ProducesNotFound]
        public async Task<IActionResult> Update(int groupId, GroupUpdateRequest request)
        {
            await groupService.UpdateGroup(groupId, request);
            return NoContent();
        }

        [HttpDelete("{groupId:int}")]
        [ProducesNotFound]
        [ProducesNoContent]
        public async Task<IActionResult> Delete(int groupId)
        {
            await groupService.DeleteGroup(groupId);
            return NoContent();
        }
    }
}
