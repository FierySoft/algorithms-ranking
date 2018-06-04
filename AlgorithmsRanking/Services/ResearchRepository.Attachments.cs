using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<Attachment[]> GetAttachmentsForDataSetAsync(int dataSetId)
        {
            return _db.Attachments.Where(x => x.DataSetId == dataSetId).ToArrayAsync();
        }

        public async Task<Attachment> CreateAttachmentAsync(Attachment model)
        {
            var create = _db.Attachments.Add(model).Entity;

            await _db.SaveChangesAsync();

            return create;
        }

        public Task CreateAttachmentsAsync(IEnumerable<Attachment> items)
        {
            _db.Attachments.AddRange(items);

            return _db.SaveChangesAsync();
        }
    }
}
