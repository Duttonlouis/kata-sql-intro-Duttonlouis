using System;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=localhost;Database=adventureworks;Uid=root;Pwd=passwordtruecoder;";
            var repo = new ProductRepository(connectionString);
            //var repo1 = new DapperProductRepo(connectionString);

            foreach (var prod in repo.GetProductsAndReviews())
            {
                Console.WriteLine("\n\n\n\nProduct Name:" + prod.Name + "\n\t\tComments:" + prod.Comments);
            }
            /*foreach (var prod in repo1.GetProductsWithReview())
            {
                Console.WriteLine("\n\n\n\nProduct Name:" + prod.Name + "\n\t\tComments:" + prod.Comments);
            } */


            //repo.DeleteProduct(324);

            Console.ReadLine();
        }

       
    }
}
