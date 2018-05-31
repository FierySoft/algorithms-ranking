﻿using System.Linq;
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
            var create = _db.Algorithms.Add(model).Entity;

            await _db.SaveChangesAsync();

            return create;
        }

        public async Task<Algorithm> UpdateAlgorithmAsync(int id, Algorithm model)
        {
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
    }
}
