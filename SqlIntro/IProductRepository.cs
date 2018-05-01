using System;
using System.Collections.Generic;
using System.Text;

namespace SqlIntro
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int id);
        void UpdateProduct(Product prod);
        void InsertProduct(Product prod);
        IEnumerable<Product>GetProductsWithReview();
        IEnumerable<Product>GetProductsAndReviews();
    }
}
