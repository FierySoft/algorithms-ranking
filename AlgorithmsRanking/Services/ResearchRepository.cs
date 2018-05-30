using AlgorithmsRanking.DbContexts;

namespace AlgorithmsRanking.Services
{
    public partial class ResearchRepository
    {
        private readonly ResearchRepositoryDbContext _db;

        public ResearchRepository(ResearchRepositoryDbContext db)
        {
            _db = db;
        }
    }
}
