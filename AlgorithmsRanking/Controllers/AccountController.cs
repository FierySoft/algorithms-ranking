using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AlgorithmsRanking.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet("who-am-i")]
        public IActionResult WhoAmI()
        {
            return new JsonResult(new
            {
                UserName = User?.Identity?.Name,
                Roles = new string[] { User?.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value },
                PhotoUri = "images/dev-user.jpg"
            });
        }
    }
}
