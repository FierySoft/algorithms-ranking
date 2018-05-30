using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Entities;
    using AlgorithmsRanking.Services;

    [Route("api/[controller]")]
    public class AlgorithmsController : Controller
    {
        private readonly ResearchRepository _db;

        public AlgorithmsController(ResearchRepository repository)
        {
            _db = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetAlgorithmsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _db.GetAlgorithmAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Algorithm model)
        {
            try
            {
                return Ok(await _db.CreateAlgorithmAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]Algorithm model)
        {
            try
            {
                return Ok(await _db.UpdateAlgorithmAsync(id, model));
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
                await _db.RemoveAlgorithmAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
