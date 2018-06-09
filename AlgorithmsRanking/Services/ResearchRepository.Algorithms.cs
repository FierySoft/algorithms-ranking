using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;
using AlgorithmsRanking.Models;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<List<EntityListItem>> GetAlgorithmsListItemsAsync()
        {
            return _db.Algorithms.Select(x => new EntityListItem(x.Id, x.Name)).ToListAsync();
        }

        public Task<Algorithm[]> GetAlgorithmsAsync()
        {
            return _db.Algorithms.ToArrayAsync();
        }

        public Task<Algorithm> GetAlgorithmAsync(int id)
        {
            return _db.Algorithms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Algorithm> CreateAlgorithmAsync(Algorithm model)
        {
            if (await CheckAlgorithmNameAndTypeExistsAsync(model.Name, model.Type))
            {
                throw new ArgumentException("Алгоритм с такими параметрами уже существует");
            }

            var create = _db.Algorithms.Add(model).Entity;

            await _db.SaveChangesAsync();

            return create;
        }

        public async Task<Algorithm> UpdateAlgorithmAsync(int id, Algorithm model)
        {
            if (await CheckAlgorithmNameAndTypeExistsAsync(id, model.Name, model.Type))
            {
                throw new ArgumentException("Алгоритм с такими параметрами уже существует");
            }

            var update = await GetAlgorithmAsync(id);

            update.Name = model.Name;
            update.Type = model.Type;

            _db.Algorithms.Update(update);
            await _db.SaveChangesAsync();

            return update;
        }

        public async Task RemoveAlgorithmAsync(int id)
        {
            var remove = await GetAlgorithmAsync(id);

            _db.Algorithms.Remove(remove);
            await _db.SaveChangesAsync();
        }


        internal Task<bool> CheckAlgorithmNameAndTypeExistsAsync(string name, string type) 
            => Task.FromResult(_db.Algorithms.Where(x => x.Name == name && x.Type == type).Any());

        internal Task<bool> CheckAlgorithmNameAndTypeExistsAsync(int id, string name, string type)
            => Task.FromResult(_db.Algorithms.Where(x => x.Id != id && x.Name == name && x.Type == type).Any());
    }
}
