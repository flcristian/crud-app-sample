using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;

namespace CrudAppSample.Products.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByNameAsync(string name);
    Task<Product> GetByIdAsync(int id);
    Task<Product> CreateAsync(CreateProductRequest productRequest);
    Task<Product> UpdateAsync(UpdateProductRequest productRequest);
    Task DeleteAsync(int id);
}