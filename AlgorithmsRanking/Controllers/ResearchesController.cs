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
            var model = new ResearchForm
            {
                Id = 0,
                Init = new ResearchInitForm(),
                Calculated = null,
                Algorithms = await _db.GetAlgorithmsListItemsAsync(),
                DataSets = await _db.GetDataSetsListItemsAsync(),
                Executors = await _db.GetPersonsListItemsAsync(),
                Permissions = new ResearchPermissions
                {
                    StatusChangeOptions = new ResearchStatus[] { ResearchStatus.ASSIGNED },
                    CanEditInit = true,
                    CanEditCalculated = false,
                    CanPostComment = false
                }
            };

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ResearchInitForm model)
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

            var model = new ResearchForm
            {
                Id = item.Id,
                Init = new ResearchInitForm
                {
                    Name = item.Name,
                    Description = item.Description,
                    AlgorithmId = item.AlgorithmId,
                    Algorithm = item.Algorithm,
                    DataSetId = item.DataSetId,
                    DataSet = item.DataSet,
                    CreatorId = item.CreatorId,
                    Creator = item.Creator,
                    ExecutorId = item.ExecutorId,
                    Executor = item.Executor
                },
                Calculated = new ResearchCalculatedForm
                {
                    AccuracyRate = item.AccuracyRate ?? 0.0f,
                    EfficiencyRate = item.EfficiencyRate ?? 0.0f
                },
                Algorithms = await _db.GetAlgorithmsListItemsAsync(),
                DataSets = await _db.GetDataSetsListItemsAsync(),
                Executors = await _db.GetPersonsListItemsAsync(),
                Permissions = permissionsService.Get(item)
            };

            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]ResearchInitForm model)
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