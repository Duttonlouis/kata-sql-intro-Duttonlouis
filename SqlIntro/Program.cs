using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
#if DEBUG
                  .AddJsonFile("appsettings.Debug.json")
#else
                  .AddJsonFile($"appsettings.Release.json")
#endif
                .Build();   

            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var repo = new DapperProductRepo(connectionString);

            foreach (var prod in repo.GetProductsAndReviews())
            {
                Console.WriteLine("\n\n\n\nProduct Name:" + prod.Name + "\n\t\tComments:" + prod.Comments);
            }
            foreach (var prod in repo.GetProductsWithReview())
            {
                Console.WriteLine("\n\n\n\nProduct Name:" + prod.Name + "\n\t\tComments:" + prod.Comments);
            } 
            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("\nProduct Name:" + prod.Name);
            }

            Console.ReadLine();
        }
    }
}
