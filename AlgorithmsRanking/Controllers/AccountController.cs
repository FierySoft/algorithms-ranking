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
                PhotoUri = "images/dev-user.jpg"
            });
        }
    }
}
