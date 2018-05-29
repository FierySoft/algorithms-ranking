using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlgorithmsRanking.Controllers
{
    using AlgorithmsRanking.Services;

    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private readonly TicketsService _tickets;

        public TicketsController(TicketsService tickets)
        {
            _tickets = tickets;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tickets.GetByAuthor("Me"));
        }
    }
}
