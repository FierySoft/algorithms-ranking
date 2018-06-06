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
    public class DataSetsController : Controller
    {
        private readonly ResearchRepository _db;

        public DataSetsController(ResearchRepository repository)
        {
            _db = repository;
        }


        [Authorize(Policy = "ReadOnlyAccess")]
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var items = await _db.GetDataSetsListItemsAsync();

            return Ok(items);
        }

        [Authorize(Policy = "ReadOnlyAccess")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _db.GetDataSetsAsync();

            return Ok(items);
        }

        [Authorize(Policy = "FullAccess")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _db.GetDataSetAsync(id);

                if (model == null)
                {
                    return NotFound(new ApiError("404", "Not Found", $"Набор данных #{id} не найден"));
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [Authorize(Policy = "FullAccess")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]DataSet model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(DataSet)} cannot be null"));
            }

            try
            {
                var item = await _db.CreateDataSetAsync(model);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [Authorize(Policy = "FullAccess")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]DataSet model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(DataSet)} cannot be null"));
            }

            if ((await _db.GetDataSetAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Набор данных #{id} не найден"));
            }

            try
            {
                return Ok(await _db.UpdateDataSetAsync(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [Authorize(Policy = "FullAccess")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if ((await _db.GetDataSetAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Набор данных #{id} не найден"));
            }

            try
            {
                await _db.RemoveDataSetAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
    }
}
