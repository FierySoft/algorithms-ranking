using System.Collections.Generic;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Models
{
    public class ResearchForm
    {
        public int Id { get; set; }
        public Research Research { get; set; }
        public ResearchInitForm Init { get; set; }
        public ResearchCalculatedForm Calculated { get; set; }
        public IEnumerable<EntityListItem> Algorithms { get; set; }
        public IEnumerable<EntityListItem> DataSets { get; set; }
        public IEnumerable<EntityListItem> Executors { get; set; }
        public ResearchPermissions Permissions { get; set; }
    }
}
