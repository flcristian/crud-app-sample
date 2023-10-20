using CrudAppSample.Products.Exceptions;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Repository;
using CrudAppSample.Products.Service.Interfaces;
using CrudAppSample.System.Constants;

namespace CrudAppSample.Products.Service;

public class ProductQueryService:IProductQueryService
{
    private IProductRepository _repository;

    
    
    public ProductQueryService(IProductRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        IEnumerable<Product> products = await _repository.GetAllAsync();

        if (products.Count() == 0)
        {
            throw new BadRequest(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }
}