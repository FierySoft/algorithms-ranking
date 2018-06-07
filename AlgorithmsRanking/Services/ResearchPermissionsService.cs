using AlgorithmsRanking.Entities;
using AlgorithmsRanking.Models;

namespace AlgorithmsRanking.Services
{
    public class ResearchPermissionsService
    {
        public ResearchPermissions Get(Research research, int userId, bool isAdmin)
        {
            return new ResearchPermissions
            {
                CanRead = GetCanRead(research, userId, isAdmin),
                CanEditInit = research.Status == ResearchStatus.OPENED && userId == research.CreatorId,
                CanEditCalculated = research.Status == ResearchStatus.IN_PROGRESS && userId == research.ExecutorId,
                CanPostComment = GetCanPostComment(research.Status, isAdmin),
                StatusChangeOptions = GetChangeStatusOptions(research, userId)
            };
        }


        private bool GetCanRead(Research research, int userId, bool isAdmin = false)
        {
            if (userId == research.CreatorId || isAdmin)
            {
                return true;
            }

            if (userId != research.ExecutorId)
            {
                return false;
            }

            return research.Status > ResearchStatus.OPENED;
        }

        private bool GetCanPostComment(ResearchStatus status, bool isAdmin)
        {
            if (isAdmin)
            {
                return ResearchStatus.ASSIGNED.Equals(status)
                    || ResearchStatus.IN_PROGRESS.Equals(status)
                    || ResearchStatus.EXECUTED.Equals(status);
            }
            else
            {
                return ResearchStatus.IN_PROGRESS.Equals(status);
            }
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
