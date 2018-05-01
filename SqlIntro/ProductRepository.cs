using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product>GetProducts()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID as ID, Name from product;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString() };
                }
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            
            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE from product where productID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                //Write a delete statement that deletes by id
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            //This is annoying and unnecessarily tedious for large objects.
            //More on this in the future...  Nothing to do here..
            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE product set name = @name where id = @id";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.Parameters.AddWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT into product (name) values(@name)";
                cmd.Parameters.AddWithValue("@name", prod.Name);
                cmd.ExecuteNonQuery();
            }
        }
        public IEnumerable<Product>GetProductsWithReview()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.Name, pr.Comments FROM product as p INNER JOIN productreview AS pr on p.ProductID = pr.ProductId;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString(), Comments = dr["Comments"].ToString() };

                    //yield return new Product { Comments = dr["Comments"].ToString() };
                }
            }
        }
        public IEnumerable<Product> GetProductsAndReviews()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.Name, pr.Comments FROM product as p LEFT JOIN productreview AS pr on p.ProductID = pr.ProductId;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString(), Comments = dr["Comments"].ToString() };

                }
            }
        }  
    }
}
