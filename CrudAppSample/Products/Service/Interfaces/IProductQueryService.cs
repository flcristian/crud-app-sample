using CrudAppSample.Products.Model;

namespace CrudAppSample.Products.Service.Interfaces;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetProductsWithCategory(string category);
    Task<IEnumerable<Product>> GetProductsWithNoCategory();
    Task<IEnumerable<Product>> GetProductsInPriceRange(double min, double max);
    Task<Product> GetProductById(int id);
}