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
    public class AlgorithmsController : Controller
    {
        private readonly ResearchRepository _db;

        public AlgorithmsController(ResearchRepository repository)
        {
            _db = repository;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var items = await _db.GetAlgorithmsListItemsAsync();

            return Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _db.GetAlgorithmsAsync();

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _db.GetAlgorithmAsync(id);

            if (model == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Алгоритм #{id} не найден"));
            }

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Algorithm model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(Algorithm)} cannot be null"));
            }

            try
            {
                return Ok(await _db.CreateAlgorithmAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]Algorithm model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(Algorithm)} cannot be null"));
            }

            if ((await _db.GetAlgorithmAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Алгоритм #{id} не найден"));
            }

            try
            {
                return Ok(await _db.UpdateAlgorithmAsync(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if ((await _db.GetAlgorithmAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Алгоритм #{id} не найден"));
            }

            try
            {
                await _db.RemoveAlgorithmAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
    }
}
