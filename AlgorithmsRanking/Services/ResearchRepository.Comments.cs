using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        public Task<Comment[]> GetCommentsAsync(int researchId)
        {
            return _db.Comments
                .Where(x => x.ResearchId == researchId && x.IsDeleted == false)
                .ToArrayAsync();
        }

        public Task<Comment[]> GetCommentsAsync(int researchId, string userName)
        {
            return _db.Comments
                .Where(x => x.ResearchId == researchId && x.Author == userName && x.IsDeleted == false)
                .ToArrayAsync();
        }

        public Task<Comment> GetCommentAsync(long id)
        {
            return _db.Comments
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
        }

        public async Task<Comment> CreateCommentAsync(Comment model)
        {
            model.Author = model.Author;
            model.PostedAt = DateTime.Now;
            model.IsDeleted = false;

            var comment = _db.Add(model).Entity;
            await _db.SaveChangesAsync();

            return comment;
        }

        public async Task UpdateCommentContentAsync(long id, string content)
        {
            var update = await GetCommentAsync(id);

            update.Content = content;

            _db.Update(update);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(long id)
        {
            var comment = await GetCommentAsync(id);

            comment.IsDeleted = true;

            _db.Update(comment);
            await _db.SaveChangesAsync();
        }
    }
}
