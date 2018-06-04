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
        public Task<List<EntityListItem>> GetDataSetsListItemsAsync()
        {
            return _db.DataSets.Select(x => new EntityListItem(x.Id, x.Name)).ToListAsync();
        }

        public Task<DataSet[]> GetDataSetsAsync()
        {
            return _db.DataSets.ToArrayAsync();
        }

        public async Task<DataSet> GetDataSetAsync(int id)
        {
            var item = await _db.DataSets.FirstOrDefaultAsync(x => x.Id == id);

            item.Files = (await GetAttachmentsForDataSetAsync(item.Id)).Select(x => x.Url).ToArray();

            return item;
        }

        public async Task<DataSet> CreateDataSetAsync(DataSet model)
        {
            var create = _db.DataSets.Add(model).Entity;
            await _db.SaveChangesAsync();

            var files = model.Files.Select(url => new Attachment(create.Id, url));
            await CreateAttachmentsAsync(files);

            return create;
        }

        public async Task<DataSet> UpdateDataSetAsync(int id, DataSet model)
        {
            var update = await GetDataSetAsync(id);

            update.Name = model.Name;
            update.Type = model.Type;

            _db.DataSets.Update(update);
            await _db.SaveChangesAsync();

            return update;
        }

        public async Task RemoveDataSetAsync(int id)
        {
            var remove = await GetDataSetAsync(id);

            _db.DataSets.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
