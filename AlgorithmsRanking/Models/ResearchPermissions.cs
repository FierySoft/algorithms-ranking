using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Models
{
    public class ResearchPermissions
    {
        public ResearchStatus[] StatusChangeOptions { get; set; }
        public bool CanEditInput { get; set; }
        public bool CanEditOutput { get; set; }


        public ResearchPermissions()
        {

        }
    }
}
