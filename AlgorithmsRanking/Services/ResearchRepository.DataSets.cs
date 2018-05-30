using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<DataSet[]> GetDataSetsAsync()
        {
            return _db.DataSets.ToArrayAsync();
        }

        public Task<DataSet> GetDataSetAsync(int id)
        {
            return _db.DataSets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DataSet> CreateDataSetAsync(DataSet model)
        {
            var create = _db.DataSets.Add(model).Entity;

            await _db.SaveChangesAsync();

            return create;
        }

        public async Task<DataSet> UpdateDataSetAsync(int id, DataSet model)
        {
            var update = await GetDataSetAsync(id);

            _db.DataSets.Update(update);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task RemoveDataSetAsync(int id)
        {
            var remove = await GetDataSetAsync(id);

            _db.DataSets.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
