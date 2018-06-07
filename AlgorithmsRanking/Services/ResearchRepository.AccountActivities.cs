using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;
using System.Linq;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<AccountActivity[]> GetAccountActivitiesAsync()
        {
            return _db.AccountActivities
                .Include(x => x.Account)
                .ToArrayAsync();
        }

        public async Task<AccountActivity[]> GetAccountActivitiesAsync(int accountId)
        {
            return await _db.AccountActivities
                .Include(x => x.Account)
                .Where(x => x.AccountId == accountId)
                .ToArrayAsync();
        }

        public async Task<AccountActivity> CreateAccountActivityAsync(AccountActivity model)
        {
            var create = _db.AccountActivities.Add(model).Entity;
            await _db.SaveChangesAsync();

            return create;
        }
    }
}
