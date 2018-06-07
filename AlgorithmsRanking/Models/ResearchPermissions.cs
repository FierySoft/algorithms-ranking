using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Models
{
    public class ResearchPermissions
    {
        public bool CanRead { get; set; }
        public bool CanEditInit { get; set; }
        public bool CanEditCalculated { get; set; }
        public bool CanPostComment { get; set; }
        public ResearchStatus[] StatusChangeOptions { get; set; }


        public ResearchPermissions()
        {

        }
    }
}
