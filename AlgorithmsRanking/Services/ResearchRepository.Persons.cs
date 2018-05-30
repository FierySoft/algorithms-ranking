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

            _db.Persons.Update(update);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task RemovePersonAsync(int id)
        {
            var remove = await GetPersonAsync(id);

            _db.Persons.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
