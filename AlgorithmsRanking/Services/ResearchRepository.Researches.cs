using System;
using System.Linq;
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

        public Task<Research[]> GetResearchesForCreatorAsync(int creatorId)
        {
            return _db.Researches
                .Include(x => x.Creator)
                .Include(x => x.Executor)
                .Include(x => x.Algorithm)
                .Include(x => x.DataSet)
                .Where(x => x.CreatorId == creatorId)
                .ToArrayAsync();
        }

        public Task<Research[]> GetResearchesForExecutorAsync(int executorId)
        {
            return _db.Researches
                .Include(x => x.Creator)
                .Include(x => x.Executor)
                .Include(x => x.Algorithm)
                .Include(x => x.DataSet)
                .Where(x => x.ExecutorId == executorId)
                .ToArrayAsync();
        }

        public async Task<Research> GetResearchAsync(int id, bool includeProperties = true)
        {
            if (includeProperties)
            {
                var result = await _db.Researches
                    .Include(x => x.Creator)
                    .Include(x => x.Executor)
                    .Include(x => x.Algorithm)
                    .Include(x => x.DataSet)
                    .FirstOrDefaultAsync(x => x.Id == id);

                // TODO: fix!
                result.DataSet.Files = await GetAttachmentsForDataSetAsync(result.DataSetId);
                result.AccuracyRates = await GetAccuracyRatesForAsync(result.Id);
                result.EfficiencyRates = await GetEfficiencyRatesForAsync(result.Id);

                return result;
            }

            return await _db.Researches.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Research> CreateResearchAsync(ResearchInitForm model)
        {
            var create = new Research
            {
                Name = model.Name,
                Description = model.Description,
                AlgorithmId = model.AlgorithmId,
                DataSetId = model.DataSetId,
                CreatorId = model.CreatorId,
                CreatedAt = DateTime.Now,
                Status = ResearchStatus.OPENED,
            };

            var result = _db.Researches.Add(create).Entity;

            await _db.SaveChangesAsync();

            return result;
        }

        public async Task<Research> UpdateResearchAsync(int id, ResearchInitForm model)
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

        public async Task<Research> AssignResearchToAsync(int id, int executorId)
        {
            var task = await GetResearchAsync(id);

            task.ExecutorId = executorId;
            task.AssignedAt = DateTime.Now;
            task.Status = ResearchStatus.ASSIGNED;

            _db.Researches.Update(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<Research> StartResearchAsync(int id)
        {
            var task = await GetResearchAsync(id);

            task.StartedAt = DateTime.Now;
            task.Status = ResearchStatus.IN_PROGRESS;

            _db.Researches.Update(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<Research> ExecuteResearchAsync(int id, ResearchCalculatedForm rates)
        {
            var task = await GetResearchAsync(id);

            if (task.AccuracyRates?.Length > 0 || task.EfficiencyRates?.Length > 0)
            {
                await RemoveRatesForAsync(id);
            }

            var items = rates.AccuracyRates.Concat(rates.EfficiencyRates);

            await CreateRatesForAsync(id, items);

            task.AccuracyRates = rates.AccuracyRates;
            task.EfficiencyRates = rates.EfficiencyRates;
            task.ExecutedAt = DateTime.Now;
            task.Status = ResearchStatus.EXECUTED;

            _db.Researches.Update(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<Research> DeclineResearchAsync(int id)
        {
            var task = await GetResearchAsync(id);

            task.ExecutedAt = null;
            task.Status = ResearchStatus.DECLINED;

            _db.Researches.Update(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task<Research> CloseResearchAsync(int id)
        {
            var task = await GetResearchAsync(id);

            task.ClosedAt = DateTime.Now;
            task.Status = ResearchStatus.CLOSED;

            _db.Researches.Update(task);
            await _db.SaveChangesAsync();

            return task;
        }
    }
}
