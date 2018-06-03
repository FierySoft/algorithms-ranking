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
                CanEditInit = research.Status == ResearchStatus.OPENED,
                CanEditCalculated = research.Status == ResearchStatus.IN_PROGRESS,
                CanPostComment = GetCanPostComment(research.Status)
            };
        }

        private ResearchStatus[] GetChangeStatusOptions(ResearchStatus currentStatus)
        {
            if (currentStatus == ResearchStatus.CLOSED)
                return new ResearchStatus[] { };

            if (currentStatus == ResearchStatus.EXECUTED)
                return new ResearchStatus[] { ResearchStatus.DECLINED, ResearchStatus.CLOSED };

            return new ResearchStatus[] { ++currentStatus };
        }

        private bool GetCanPostComment(ResearchStatus status)
        {
            return ResearchStatus.ASSIGNED.Equals(status)
                || ResearchStatus.IN_PROGRESS.Equals(status)
                || ResearchStatus.EXECUTED.Equals(status);
        }
    }
}
