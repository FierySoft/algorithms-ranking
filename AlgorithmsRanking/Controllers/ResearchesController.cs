using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Models;
    using AlgorithmsRanking.Services;

    [Authorize]
    [Route("api/[controller]")]
    public class ResearchesController : Controller
    {
        private readonly ResearchRepository _db;

        public ResearchesController(ResearchRepository repository)
        {
            _db = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.GetResearchesAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _db.GetResearchAsync(id));
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var model = new ResearchUpdateForm();
            var algorithms = await _db.GetAlgorithmsListItemsAsync();
            var dataSets = await _db.GetDataSetsListItemsAsync();

            return Ok(new { model, algorithms, dataSets });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ResearchCreateForm model)
        {
            try
            {
                return Ok(await _db.CreateResearchAsync(model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:int}/permissions")]
        public async Task<IActionResult> Permissions([FromRoute]int id, [FromServices]ResearchPermissionsService permissions)
        {
            var research = await _db.GetResearchAsync(id);

            return Ok(permissions.Get(research));
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromServices]ResearchPermissionsService permissionsService)
        {
            var item = await _db.GetResearchAsync(id);
            var model = new ResearchUpdateForm
            {
                Name = item.Name,
                Description = item.Description,
                AlgorithmId = item.AlgorithmId,
                DataSetId = item.DataSetId,
                ExecutorId = item.ExecutorId
            };

            var algorithms = await _db.GetAlgorithmsListItemsAsync();
            var dataSets = await _db.GetDataSetsListItemsAsync();
            var permissions = permissionsService.Get(item);

            return Ok(new { model, algorithms, dataSets, permissions });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]ResearchUpdateForm model)
        {
            try
            {
                return Ok(await _db.UpdateResearchAsync(id, model));
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
                await _db.RemoveResearchAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}