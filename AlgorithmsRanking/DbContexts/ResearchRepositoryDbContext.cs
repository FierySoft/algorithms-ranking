using Microsoft.EntityFrameworkCore;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.DbContexts
{
    public class ResearchRepositoryDbContext : DbContext
    {
        public ResearchRepositoryDbContext(DbContextOptions<ResearchRepositoryDbContext> options)
            : base(options)
        {

        }


        public DbSet<Person> Persons { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<Research> Researches { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>().Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Entity<Person>().Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Entity<Person>().Property(p => p.MiddleName)
                .HasMaxLength(20);
         
            builder.Entity<Person>().Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Entity<Person>().Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(12);


            builder.Entity<Algorithm>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Algorithm>().Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(25);


            builder.Entity<DataSet>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<DataSet>().Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(25);


            builder.Entity<Research>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Research>().Property(x => x.Description)
                .HasMaxLength(500);

            builder.Entity<Research>().HasKey(x => new { x.Id });
        }
    }
}
