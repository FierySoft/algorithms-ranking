namespace AlgorithmsRanking.Models
{
    public class ResearchInitForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AlgorithmId { get; set; }
        public int DataSetId { get; set; }
        public int CreatorId { get; set; }
        public int? ExecutorId { get; set; }
    }
}
