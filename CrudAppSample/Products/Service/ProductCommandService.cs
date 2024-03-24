using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Repository;
using CrudAppSample.System.Constants;
using CrudAppSample.System.Exceptions;

namespace CrudAppSample.Products.Service.Interfaces;

public class ProductCommandService : IProductCommandService
{
    private IProductRepository _repository;

    public ProductCommandService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> CreateProduct(CreateProductRequest productRequest)
    {
        if (productRequest.Price < 0)
        {
            throw new InvalidPrice(Constants.INVALID_PRICE);
        }
        
        Product product = await _repository.GetByNameAsync(productRequest.Name);
        if (product != null)
        {
            throw new ItemAlreadyExists(Constants.PRODUCT_ALREADY_EXISTS);
        }

        product = await _repository.CreateAsync(productRequest);
        return product;
    }

    public async Task<Product> UpdateProduct(UpdateProductRequest productRequest)
    {
        if (productRequest.Price < 0)
        {
            throw new InvalidPrice(Constants.INVALID_PRICE);
        }
        
        Product product = await _repository.GetByIdAsync(productRequest.Id);
        if (product == null)
        {
            throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
        }
        product = await _repository.UpdateAsync(productRequest);
        return product;
    }

    public async Task<Product> DeleteProduct(int id)
    {
        Product product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            throw new ItemDoesNotExist(Constants.PRODUCT_DOES_NOT_EXIST);
        }

        await _repository.DeleteAsync(id);
        return product;
    }
}