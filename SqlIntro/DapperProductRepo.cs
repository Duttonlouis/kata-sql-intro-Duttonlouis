using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;
using System.Linq;

namespace SqlIntro
{
    
    public class DapperProductRepo : IProductRepository
    {
        private readonly string _connectionString;

        public DapperProductRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void DeleteProduct(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("DELETE from product where productID = @id", new { id });
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT ProductID as ID, Name from product;");
            }
        }

        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("INSERT product set name = @name where id = @id", new { prod });
            }
        }

        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("UPDATE product set name = @name where id = @id", new { prod });
            }
        }
        public IEnumerable<Product>GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                /*var sql = "SELECT * FROM Products AS A INNER JOIN ProductsReview AS B ON A.ReviewID = B.ReviewID;";
                var review = conn.Query<Product, ProductReview, Product>(sql, (review, ProductReview) =>
                {
                    review.ProductReview = productreview;
                    return review;
                }

                    , splitOn: "ReviewID").Distinct().ToList();*/
                var sql = "SELECT p.Name, pr.Comments FROM product as p INNER JOIN productreview AS pr on p.ProductID = pr.ProductId;";
                return conn.Query<Product>(sql);

           

            }
        }
        public IEnumerable<Product>GetProductsAndReviews()

        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT p.Name, pr.Comments FROM product as p LEFT JOIN productreview AS pr on p.ProductID = pr.ProductId;");



            }
        }

    }
}
