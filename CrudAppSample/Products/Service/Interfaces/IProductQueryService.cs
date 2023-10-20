using CrudAppSample.Products.Model;

namespace CrudAppSample.Products.Service.Interfaces;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> GetAllProducts();
}