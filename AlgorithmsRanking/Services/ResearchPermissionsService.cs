using AlgorithmsRanking.Entities;
using AlgorithmsRanking.Models;

namespace AlgorithmsRanking.Services
{
    public class ResearchPermissionsService
    {
        public ResearchPermissions Get(Research research)
        {
            return new ResearchPermissions
            {
                StatusChangeOptions = GetChangeStatusOptions(research.Status),
                CanEditInput = research.Status == ResearchStatus.OPENED,
                CanEditOutput = research.Status == ResearchStatus.IN_PROGRESS,
            };
        }

        private ResearchStatus[] GetChangeStatusOptions(ResearchStatus currentStatus)
        {
            return currentStatus == ResearchStatus.EXECUTED ?
                new ResearchStatus[] { ResearchStatus.DECLINED, ResearchStatus.CLOSED } :
                new ResearchStatus[] { ++currentStatus };
        }
    }
}
