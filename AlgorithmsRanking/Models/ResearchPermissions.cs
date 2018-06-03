using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Models
{
    public class ResearchPermissions
    {
        public ResearchStatus[] StatusChangeOptions { get; set; }
        public bool CanEditInit { get; set; }
        public bool CanEditCalculated { get; set; }
        public bool CanPostComment { get; set; }


        public ResearchPermissions()
        {

        }
    }
}
