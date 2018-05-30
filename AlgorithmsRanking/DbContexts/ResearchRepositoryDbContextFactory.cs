using AlgorithmsRanking.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AlgorithmsRanking
{
    public class ResearchRepositoryDbContextFactory : IDesignTimeDbContextFactory<ResearchRepositoryDbContext>
    {
        public ResearchRepositoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ResearchRepositoryDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ResearchDb;Trusted_Connection=True;ConnectRetryCount=0");

            return new ResearchRepositoryDbContext(optionsBuilder.Options);
        }
    }
}
