using System;

namespace AlgorithmsRanking.Entities
{
    public class Research
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int CreatorId { get; set; }
        public virtual Person Creator { get; set; }

        public int? ExecutorId { get; set; }
        public virtual Person Executor { get; set; }

        public int AlgorithmId { get; set; }
        public virtual Algorithm Algorithm { get; set; }

        public int DataSetId { get; set; }
        public virtual DataSet DataSet { get; set; }

        public float? AccuracyRate { get; set; }
        public float? EfficiencyRate { get; set; }

        public ResearchStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
