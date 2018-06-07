using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Models;
    using AlgorithmsRanking.Entities;
    using AlgorithmsRanking.Services;

    [Authorize]
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly ResearchRepository _db;

        public PersonsController(ResearchRepository repository)
        {
            _db = repository;
        }


        [Authorize(Policy = "ReadOnlyAccess")]
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var items = await _db.GetPersonsListItemsAsync();

            return Ok(items);
        }

        [Authorize(Policy = "ReadOnlyAccess")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _db.GetAccountsAsync();

            return Ok(items);
        }

        [Authorize(Policy = "FullAccess")]
        [HttpGet("{personId:int}")]
        public async Task<IActionResult> Get(int personId)
        {
            var model = await _db.GetAccountByPersonIdAsync(personId);

            if (model == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Учетная запись для участника #{personId} не найдена"));
            }

            return Ok(model);
        }

        [Authorize(Policy = "FullAccess")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Account model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(Account)} cannot be null"));
            }

            try
            {
                // TODO: add photo upload!
                model.AvatarUri = "/images/dev-user.jpg";

                return Ok(await _db.CreateAccountAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [Authorize(Policy = "FullAccess")]
        [HttpPut("{personId:int}")]
        public async Task<IActionResult> Edit([FromRoute]int personId, [FromBody]Account model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(Account)} cannot be null"));
            }

            if ((await _db.GetAccountByPersonIdAsync(personId)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Учетная запись для участника #{personId} не найдена"));
            }

            try
            {
                return Ok(await _db.UpdateAccountAsync(model.Id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [Authorize(Policy = "FullAccess")]
        [HttpDelete("{personId:int}")]
        public async Task<IActionResult> Delete([FromRoute]int personId)
        {
            var model = await _db.GetAccountByPersonIdAsync(personId);

            if (model == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Учетная запись для участника #{personId} не найдена"));
            }

            try
            {
                await _db.RemoveAccountAsync(model.Id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
    }
}
