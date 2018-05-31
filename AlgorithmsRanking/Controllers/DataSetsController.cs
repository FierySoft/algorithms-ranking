using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Entities;
    using AlgorithmsRanking.Services;

    [Route("api/[controller]")]
    public class DataSetsController : Controller
    {
        private readonly ResearchRepository _db;

        public DataSetsController(ResearchRepository repository)
        {
            _db = repository;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok(await _db.GetDataSetsListItemsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetDataSetsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _db.GetDataSetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]DataSet model)
        {
            try
            {
                return Ok(await _db.CreateDataSetAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]DataSet model)
        {
            try
            {
                return Ok(await _db.UpdateDataSetAsync(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _db.RemoveDataSetAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
