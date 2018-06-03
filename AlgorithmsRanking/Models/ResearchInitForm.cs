using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Models
{
    public class ResearchInitForm
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int AlgorithmId { get; set; }
        public Algorithm Algorithm { get; set; }

        public int DataSetId { get; set; }
        public DataSet DataSet { get; set; }

        public int CreatorId { get; set; }
        public Person Creator { get; set; }

        public int? ExecutorId { get; set; }
        public Person Executor { get; set; }
    }
}
