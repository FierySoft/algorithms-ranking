using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
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

            _db.Algorithms.Update(update);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task RemoveAlgorithmAsync(int id)
        {
            var remove = await GetAlgorithmAsync(id);

            _db.Algorithms.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
