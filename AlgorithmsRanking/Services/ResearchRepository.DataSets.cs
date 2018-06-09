using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;
using AlgorithmsRanking.Models;
using System;

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

            item.Files = await GetAttachmentsForDataSetAsync(item.Id);

            return item;
        }

        public async Task<DataSet> CreateDataSetAsync(DataSet model)
        {
            if (await CheckDataSetNameAndTypeExistsAsync(model.Name, model.Type))
            {
                throw new ArgumentException("Набор данных с такими параметрами уже существует");
            }

            var create = _db.DataSets.Add(model).Entity;
            await _db.SaveChangesAsync();

            var files = model.Files.Select(file => { file.DataSetId = create.Id; return file; });
            await CreateAttachmentsAsync(files);

            return create;
        }

        public async Task<DataSet> UpdateDataSetAsync(int id, DataSet model)
        {
            if (await CheckDataSetNameAndTypeExistsAsync(id, model.Name, model.Type))
            {
                throw new ArgumentException("Набор данных с такими параметрами уже существует");
            }

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


        internal Task<bool> CheckDataSetNameAndTypeExistsAsync(string name, string type)
            => Task.FromResult(_db.DataSets.Where(x => x.Name == name && x.Type == type).Any());

        internal Task<bool> CheckDataSetNameAndTypeExistsAsync(int id, string name, string type)
            => Task.FromResult(_db.DataSets.Where(x => x.Id != id && x.Name == name && x.Type == type).Any());
    }
}
