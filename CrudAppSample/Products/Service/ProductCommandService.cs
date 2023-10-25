using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Exceptions;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Repository;
using CrudAppSample.System.Constants;

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
        Product product = await _repository.GetByNameAsync(productRequest.Name);
        if (product != null)
        {
            throw new BadRequest(Constants.PRODUCT_ALREADY_EXISTS);
        }

        product = await _repository.CreateAsync(productRequest);
        return product;
    }

    public async Task<Product> UpdateProduct(int id, UpdateProductRequest productRequest)
    {
        Product product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            throw new BadRequest(Constants.PRODUCT_DOES_NOT_EXIST);
        }
        product = await _repository.UpdateAsync(id, productRequest);
        return product;
    }

    public async Task DeleteProduct(int id)
    {
        Product product = await _repository.GetByIdAsync(id);
        if (product == null)
        {
            throw new BadRequest(Constants.PRODUCT_DOES_NOT_EXIST);
        }

        await _repository.DeleteAsync(id);
    }
}