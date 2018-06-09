namespace AlgorithmsRanking.Entities
{
    public class ResearchRate
    {
        public int Id { get; set; }
        public int ResearchId { get; set; }
        public ResearchRateType Type { get; set; }
        public float Value { get; set; }
    }


    public enum ResearchRateType
    {
        ACCURACY,
        EFFICIENCY
    }
}
