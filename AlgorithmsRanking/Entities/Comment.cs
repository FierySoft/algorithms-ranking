using System;

namespace AlgorithmsRanking.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public int ResearchId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PostedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
