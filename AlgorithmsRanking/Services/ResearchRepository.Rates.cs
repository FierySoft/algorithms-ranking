using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial  class ResearchRepository
    {
        public Task<ResearchRate[]> GetAccuracyRatesForAsync(int researchId)
        {
            return _db.Rates.Where(x => x.ResearchId == researchId && x.Type == ResearchRateType.ACCURACY).ToArrayAsync();
        }

        public Task<ResearchRate[]> GetEfficiencyRatesForAsync(int researchId)
        {
            return _db.Rates.Where(x => x.ResearchId == researchId && x.Type == ResearchRateType.EFFICIENCY).ToArrayAsync();
        }

        public Task CreateRatesForAsync(int researchId, IEnumerable<ResearchRate> items)
        {
            _db.Rates.AddRange(items);

            return _db.SaveChangesAsync();
        }

        public async Task RemoveRatesForAsync(int researchId)
        {
            var items = await _db.Rates.Where(x => x.ResearchId == researchId).ToArrayAsync();

            _db.Rates.RemoveRange(items);

            await _db.SaveChangesAsync();
        }
    }
}
