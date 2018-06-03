using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Entities;
    using AlgorithmsRanking.Services;

    [Authorize]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ResearchRepository _db;

        public CommentsController(ResearchRepository dbService)
        {
            _db = dbService;
        }


        [HttpGet("{researchId:int}")]
        public async Task<IActionResult> Get([FromRoute]int researchId)
        {
            var comments = await _db.GetCommentsAsync(researchId);

            return Ok(comments);
        }

        [HttpPost("{researchId:int}")]
        public async Task<IActionResult> Post([FromRoute]int researchId, [FromBody]Comment comment)
        {
            var research = await _db.GetResearchAsync(researchId);

            if (research == null)
            {
                return NotFound();
            }

            comment.ResearchId = research.Id;
            comment.Author = User?.Identity?.Name; // TODO: переделать на Person

            await _db.CreateCommentAsync(comment);

            return Ok(comment);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            try
            {
                await _db.DeleteCommentAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
