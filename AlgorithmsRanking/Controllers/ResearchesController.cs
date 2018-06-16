using System;
using System.Linq;
using System.Security.Claims;
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
            var items = await _db.GetResearchesAsync();

            return Ok(items);
        }

        [HttpGet("folders")]
        public async Task<IActionResult> Folders()
        {
            Research[] researches = new Research[] { };
            var personId = Int32.Parse(User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value);

            if (User != null && User.IsInRole("Admin"))
            {
                researches = await _db.GetResearchesForCreatorAsync(personId);
            }

            if (User != null && User.IsInRole("User"))
            {
                researches = await _db.GetResearchesForExecutorAsync(personId);
            }

            var active = researches.Where(x => x.Status < ResearchStatus.EXECUTED || x.Status == ResearchStatus.DECLINED).OrderBy(x => x.Status);
            var toConfirm = researches.Where(x => x.Status == ResearchStatus.EXECUTED);
            var archive = researches.Where(x => x.Status == ResearchStatus.CLOSED);

            return Ok(new { active, toConfirm, archive });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _db.GetResearchAsync(id);

            if (model == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Исследование #{id} не найдено"));
            }

            return Ok(model);
        }

        [Authorize(Policy = "FullAccess")]
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var model = new ResearchForm
            {
                Id = 0,
                Research = new Research(),
                Init = new ResearchInitForm(),
                Calculated = null,
                Algorithms = await _db.GetAlgorithmsListItemsAsync(),
                DataSets = await _db.GetDataSetsListItemsAsync(),
                Executors = await _db.GetPersonsListItemsByRoleAsync("User"),
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

        [Authorize(Policy = "FullAccess")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ResearchInitForm model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(ResearchInitForm)} cannot be null"));
            }

            try
            {
                model.CreatorId = Int32.Parse(User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value);

                var result = await _db.CreateResearchAsync(model);

                if (model.ExecutorId.HasValue)
                {
                    result = await _db.AssignResearchToAsync(result.Id, model.ExecutorId.Value);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpGet("{id:int}/permissions")]
        public async Task<IActionResult> Permissions([FromRoute]int id, [FromServices]ResearchPermissionsService permissions)
        {
            var research = await _db.GetResearchAsync(id);
            var personId = Int32.Parse(User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value);

            return Ok(permissions.Get(research, personId));
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromServices]ResearchPermissionsService permissionsService)
        {
            var item = await _db.GetResearchAsync(id);

            if (item == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Исследование #{id} не найдено"));
            }

            var personId = Int32.Parse(User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value);
            var permissions = permissionsService.Get(item, personId);

            if (!permissions.CanRead)
            {
                return Forbid();
            }

            var model = new ResearchForm
            {
                Id = item.Id,
                Research = item,
                Init = new ResearchInitForm
                {
                    Name = item.Name,
                    Description = item.Description,
                    AlgorithmId = item.AlgorithmId,
                    DataSetId = item.DataSetId,
                    CreatorId = item.CreatorId,
                    ExecutorId = item.ExecutorId,
                },
                Calculated = item.AccuracyRates != null && item.EfficiencyRates != null ?
                    new ResearchCalculatedForm
                    {
                        AccuracyRates = item.AccuracyRates,
                        EfficiencyRates = item.EfficiencyRates
                    } : null,
                Algorithms = await _db.GetAlgorithmsListItemsAsync(),
                DataSets = await _db.GetDataSetsListItemsAsync(),
                Executors = await _db.GetPersonsListItemsByRoleAsync("User"),
                Permissions = permissions
            };

            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromBody]ResearchInitForm model)
        {
            if (model == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"{typeof(ResearchInitForm)} cannot be null"));
            }

            if ((await _db.GetResearchAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Исследование #{id} не найдено"));
            }

            try
            {
                var result = await _db.UpdateResearchAsync(id, model);

                if (model.ExecutorId.HasValue)
                {
                    result = await _db.AssignResearchToAsync(id, model.ExecutorId.Value);
                }

                return Ok(result);
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
            if ((await _db.GetResearchAsync(id)) == null)
            {
                return NotFound(new ApiError("404", "Not Found", $"Исследование #{id} не найдено"));
            }

            try
            {
                await _db.RemoveResearchAsync(id);

                return Ok(new { deleted = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpGet("{id:int}/start")]
        public async Task<IActionResult> Start([FromRoute]int id)
        {
            try
            {
                return Ok(await _db.StartResearchAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpPost("{id:int}/execute")]
        public async Task<IActionResult> Execute([FromRoute]int id, [FromBody]ResearchCalculatedForm rates)
        {
            if (rates == null)
            {
                return BadRequest(new ApiError("400", "Null model", $"Не заданы вычисленные параметры"));
            }

            try
            {
                return Ok(await _db.ExecuteResearchAsync(id, rates));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpGet("{id:int}/decline")]
        public async Task<IActionResult> Decline([FromRoute]int id)
        {
            try
            {
                return Ok(await _db.DeclineResearchAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }

        [HttpGet("{id:int}/close")]
        public async Task<IActionResult> Close([FromRoute]int id)
        {
            try
            {
                return Ok(await _db.CloseResearchAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiError(ex));
            }
        }
    }
}