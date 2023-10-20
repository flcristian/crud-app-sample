using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;

namespace CrudAppSample.Products.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByNameAsync(string name);
    Task<Product> CreateAsync(CreateProductRequest productRequest);
    // Task<Product> UpdateAsync(int id, Product product);
    // Task<bool> DeleteAsync(int id);
}