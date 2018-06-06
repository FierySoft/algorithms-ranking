using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<Account[]> GetAccountsAsync()
        {
            return _db.Accounts.ToArrayAsync();
        }

        public Task<Account> GetAccountAsync(int id)
        {
            return _db.Accounts
                .Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Account> GetAccountAsync(string userName)
        {
            return _db.Accounts
                .Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<Account> CreateAccountAsync(Account model)
        {
            model.RegisteredAt = DateTime.Now;
            var create = _db.Accounts.Add(model).Entity;

            await _db.SaveChangesAsync();

            return create;
        }

        public async Task<Account> UpdateAccountAsync(int id, Account model)
        {
            var update = await GetAccountAsync(id);
            
            update.Password = model.Password;
            update.Role = model.Role;
            update.AvatarUri = model.AvatarUri;

            _db.Accounts.Update(update);
            await _db.SaveChangesAsync();

            return update;
        }

        public async Task RemoveAccountAsync(int id)
        {
            var remove = await GetAccountAsync(id);

            _db.Accounts.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
