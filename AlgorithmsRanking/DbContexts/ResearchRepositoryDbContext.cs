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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<Research> Researches { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AccountActivity> AccountActivities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>().Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Entity<Account>().Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(25);

            builder.Entity<Account>().HasKey(x => x.Id);
            
            
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

            builder.Entity<Person>().HasKey(x => x.Id);


            builder.Entity<Algorithm>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Algorithm>().Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(25);

            builder.Entity<Algorithm>().HasKey(x => x.Id);


            builder.Entity<DataSet>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<DataSet>().Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(25);

            builder.Entity<DataSet>().HasKey(x => x.Id);

            builder.Entity<DataSet>().Ignore(c => c.Files);
            builder.Entity<DataSet>().Ignore(c => c.FilesCount);


            builder.Entity<Research>().Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Research>().Property(x => x.Description)
                .HasMaxLength(500);

            builder.Entity<Research>().HasKey(x => x.Id);


            builder.Entity<Comment>().Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(25);

            builder.Entity<Comment>().Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(500);

            builder.Entity<Comment>().HasKey(x => x.Id);


            builder.Entity<Attachment>().Property(x => x.Url)
                .IsRequired();

            builder.Entity<Attachment>().Property(x => x.Name)
                .IsRequired();

            builder.Entity<Attachment>().HasKey(x => x.Id);


            builder.Entity<AccountActivity>().Property(x => x.Operation)
                .IsRequired()
                .HasMaxLength(25);

            builder.Entity<AccountActivity>().Property(x => x.IpAddress)
                .IsRequired()
                .HasMaxLength(15);

            builder.Entity<AccountActivity>().HasKey(x => x.Id);
        }
    }
}
