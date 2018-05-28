using Microsoft.AspNetCore.Mvc;
using AlgorithmsRanking.Services;

namespace AlgorithmsRanking.Controllers
{
    public class HomeController : Controller
    {
        private readonly TicketsService _db;

        public HomeController(TicketsService db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            return View(_db.GetByAuthor("Me"));
        }
    }
}
