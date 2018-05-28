using System.Collections.Generic;
using System.Linq;
using AlgorithmsRanking.Models;

namespace AlgorithmsRanking.Services
{
    public class TicketsService
    {
        private readonly List<Ticket> _tickets;

        public TicketsService()
        {
            _tickets = Initialize();
        }

        private List<Ticket> Initialize()
        {
            return new List<Ticket>()
            {
                new Ticket("Task1", "Research bubble search algorithm", "Me", 1),
                new Ticket("Task2", "Research Huffman's algorithm", "Me", 2),
                new Ticket("Task3", "Research quick search algorithm", "Me", 3)
            };
        }


        public List<Ticket> GetAll() => _tickets;

        public List<Ticket> GetByAuthor(string author) => _tickets.Where(x => x.Author == author).ToList();

        public Ticket GetById(int id) => _tickets.FirstOrDefault(x => x.Id == id);
    }
}
