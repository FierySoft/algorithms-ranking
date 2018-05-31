namespace AlgorithmsRanking.Models
{
    public class ResearchUpdateForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ExecutorId { get; set; }
        public int AlgorithmId { get; set; }
        public int DataSetId { get; set; }
    }
}
