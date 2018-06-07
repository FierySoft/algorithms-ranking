using AlgorithmsRanking.Entities;
using AlgorithmsRanking.Models;

namespace AlgorithmsRanking.Services
{
    public class ResearchPermissionsService
    {
        public ResearchPermissions Get(Research research, int userId)
        {
            return new ResearchPermissions
            {
                CanRead = GetCanRead(research, userId),
                CanEditInit = research.Status == ResearchStatus.OPENED && userId == research.CreatorId,
                CanEditCalculated = research.Status == ResearchStatus.IN_PROGRESS && userId == research.ExecutorId,
                CanPostComment = GetCanPostComment(research, userId),
                StatusChangeOptions = GetChangeStatusOptions(research, userId)
            };
        }


        private bool GetCanRead(Research research, int userId)
        {
            if (userId == research.CreatorId)
            {
                return true;
            }

            if (userId != research.ExecutorId)
            {
                return false;
            }

            return research.Status > ResearchStatus.OPENED;
        }

        private bool GetCanPostComment(Research research, int userId)
        {
            if (userId == research.CreatorId)
            {
                return ResearchStatus.ASSIGNED.Equals(research.Status)
                    || ResearchStatus.IN_PROGRESS.Equals(research.Status)
                    || ResearchStatus.EXECUTED.Equals(research.Status);
            }

            if (userId == research.ExecutorId)
            {
                return ResearchStatus.IN_PROGRESS.Equals(research.Status);
            }

            return false;
        }

        private ResearchStatus[] GetChangeStatusOptions(Research research, int userId)
        {
            if (research.Status == ResearchStatus.CLOSED)
            {
                return new ResearchStatus[] { };
            }

            if (userId == research.CreatorId)
            {
                switch (research.Status)
                {
                    case ResearchStatus.OPENED: return new ResearchStatus[] { ResearchStatus.ASSIGNED };
                    case ResearchStatus.EXECUTED: return new ResearchStatus[] { ResearchStatus.DECLINED, ResearchStatus.CLOSED };
                    default: return new ResearchStatus[] { };
                }
            }

            if (userId == research.ExecutorId)
            {
                switch (research.Status)
                {
                    case ResearchStatus.ASSIGNED: return new ResearchStatus[] { ResearchStatus.IN_PROGRESS };
                    case ResearchStatus.DECLINED: return new ResearchStatus[] { ResearchStatus.IN_PROGRESS };
                    case ResearchStatus.IN_PROGRESS: return new ResearchStatus[] { ResearchStatus.EXECUTED };
                    default: return new ResearchStatus[] { };
                }
            }

            return new ResearchStatus[] { };
        }
    }
}
