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
            return Ok(await _db.GetPersonsListItemsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetPersonsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _db.GetPersonAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Person model)
        {
            try
            {
                return Ok(await _db.CreatePersonAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]Person model)
        {
            try
            {
                return Ok(await _db.UpdatePersonAsync(id, model));
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
                await _db.RemovePersonAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
