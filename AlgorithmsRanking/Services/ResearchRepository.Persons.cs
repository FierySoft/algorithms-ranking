using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<Person[]> GetPersonsAsync()
        {
            return _db.Persons.ToArrayAsync();
        }

        public Task<Person> GetPersonAsync(int id)
        {
            return _db.Persons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Person> CreatePersonAsync(Person model)
        {
            var create = _db.Persons.Add(model).Entity;

            await _db.SaveChangesAsync();

            return create;
        }

        public async Task<Person> UpdatePersonAsync(int id, Person model)
        {
            var update = await GetPersonAsync(id);

            update.FirstName = model.FirstName;
            update.MiddleName = model.MiddleName;
            update.LastName = model.LastName;
            update.Email = model.Email;
            update.Phone = model.Phone;

            _db.Persons.Update(update);
            await _db.SaveChangesAsync();

            return update;
        }

        public async Task RemovePersonAsync(int id)
        {
            var remove = await GetPersonAsync(id);

            _db.Persons.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
