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


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var items = await _db.GetPersonsListItemsAsync();

            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _db.GetPersonsAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _db.GetPersonAsync(id);

            if (model == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Персона #{id} не найдена"));
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Person model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(Person)} cannot be null"));
            }

            try
            {
                return Ok(await _db.CreatePersonAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]Person model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(Person)} cannot be null"));
            }

            if ((await _db.GetPersonAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Персона #{id} не найдена"));
            }

            try
            {
                return Ok(await _db.UpdatePersonAsync(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if ((await _db.GetPersonAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Персона #{id} не найдена"));
            }

            try
            {
                await _db.RemovePersonAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
    }
}
