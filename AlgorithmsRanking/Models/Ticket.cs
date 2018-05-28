using System;

namespace AlgorithmsRanking.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Rank { get; set; }

        public Ticket(string name, string description, string author, int id)
        {
            Id = id;
            Name = name;
            Description = description;
            Author = author;
            CreatedAt = DateTime.Now;
            Rank = new Random(id).NextDouble();
        }
    }
}
