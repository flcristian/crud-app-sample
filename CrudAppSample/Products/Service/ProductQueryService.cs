using CrudAppSample.Products.Model;
using CrudAppSample.Products.Repository;
using CrudAppSample.Products.Service.Interfaces;
using CrudAppSample.System.Constants;
using CrudAppSample.System.Exceptions;

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
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsWithCategory(string category)
    {
        IEnumerable<Product> products = (await _repository.GetAllAsync())
            .Where(product => product.Category.Equals(category));
        
        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsWithNoCategory()
    {
        IEnumerable<Product> products = (await _repository.GetAllAsync())
            .Where(product => product.Category == null!);

        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsInPriceRange(double min, double max)
    {
        IEnumerable<Product> products = (await _repository.GetAllAsync())
            .Where(product => product.Price >= min && product.Price <= max);
        
        if (products.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PRODUCTS_EXIST);
        }

        return products;
    }
    
    public async Task<Product> GetProductById(int id)
    {
        Product product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
        }

        return product;
    }
}