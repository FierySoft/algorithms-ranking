using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;
using AlgorithmsRanking.Models;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<Research[]> GetResearchesAsync()
        {
            return _db.Researches
                .Include(x => x.Creator)
                .Include(x => x.Executor)
                .Include(x => x.Algorithm)
                .Include(x => x.DataSet)
                .ToArrayAsync();
        }

        public Task<Research> GetResearchAsync(int id, bool includeProperties = true)
        {
            if (includeProperties)
            {
                return _db.Researches
                    .Include(x => x.Creator)
                    .Include(x => x.Executor)
                    .Include(x => x.Algorithm)
                    .Include(x => x.DataSet)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }

            return _db.Researches.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Research> CreateResearchAsync(ResearchCreateForm model)
        {
            var create = new Research
            {
                Name = model.Name,
                Description = model.Description,
                CreatorId = model.CreatorId,
                AlgorithmId = model.AlgorithmId,
                DataSetId = model.DataSetId,
                CreatedAt = DateTime.Now,
                Status = ResearchStatus.OPENED,
            };

            var result = _db.Researches.Add(create).Entity;

            await _db.SaveChangesAsync();

            return result;
        }

        public async Task<Research> UpdateResearchAsync(int id, ResearchUpdateForm model)
        {
            var update = await GetResearchAsync(id, false);

            update.Name = model.Name;
            update.Description = model.Description;
            update.AlgorithmId = model.AlgorithmId;
            update.DataSetId = model.DataSetId;

            _db.Researches.Update(update);
            await _db.SaveChangesAsync();

            return update;
        }

        public async Task RemoveResearchAsync(int id)
        {
            var remove = await GetResearchAsync(id);

            _db.Researches.Remove(remove);
            await _db.SaveChangesAsync();
        }
    }
}
