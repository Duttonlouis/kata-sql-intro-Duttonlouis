using System.Collections.Generic;
using Dapper;
using MySql.Data.MySqlClient;

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
                conn.Execute("NSERT INTO product (Name) VALUES (@name);", new { name = prod.Name });
            }
        }

        public void UpdateProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute("UPDATE product SET name = @name WHERE Productid = @id", prod );
            }
        }
        public IEnumerable<Product>GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var sql = "SELECT p.Name, pr.Comments FROM product as p INNER JOIN productreview AS pr on p.ProductID = pr.ProductId;";
                return conn.Query<Product>(sql);
            }
        }
        public IEnumerable<Product>GetProductsAndReviews()

        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                return conn.Query<Product>("SELECT p.Name, pr.Comments FROM product as p LEFT JOIN productreview AS pr on p.ProductId = pr.ProductId;");
            }
        }

    }
}
