using System.Linq;
using AlgorithmsRanking.Entities;

namespace AlgorithmsRanking.DbContexts
{
    public static class DbInitializer
    {
        public static void Initialize(ResearchRepositoryDbContext context)
        {
            // Check database exists
            context.Database.EnsureCreated();


            // Look for any persons
            if (context.Persons.Any())
            {
                return; // DB has been seeded
            }

            var persons = new Person[]
            {
                new Person("Иванов", "Иван", "Иванович", "ivanovii@mail.ru", "+79999999999")
            };

            context.Persons.AddRange(persons);
            context.SaveChanges();


            // Look for any algorithms
            if (context.Algorithms.Any())
            {
                return; // DB has been seeded
            }

            var algorithms = new Algorithm[]
            {
                new Algorithm("Кузнечик", "Шифрованный"),
                new Algorithm("Градация", "Линейный")
            };

            context.Algorithms.AddRange(algorithms);
            context.SaveChanges();


            // Look for any data sets
            if (context.DataSets.Any())
            {
                return; // DB has been seeded
            }

            var dataSets = new DataSet[]
            {
                new DataSet("Группа А", "Табличный"),
                new DataSet("Группа Б", "Графический")
            };

            context.DataSets.AddRange(dataSets);
            context.SaveChanges();
        }
    }
}
