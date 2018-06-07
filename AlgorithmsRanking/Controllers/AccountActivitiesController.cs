using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Services;

    [Authorize(Policy = "FullAccess")]
    [Route("api/account-activities")]
    public class AccountActivitiesController : Controller
    {
        private readonly ResearchRepository _db;

        public AccountActivitiesController(ResearchRepository repository)
        {
            _db = repository;
        }

        
        [HttpGet("{accountId:int}")]
        public async Task<IActionResult> Get([FromRoute]int accountId)
        {
            var items = await _db.GetAccountActivitiesAsync(accountId);

            return Ok(items);
        }
    }
}
