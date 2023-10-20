using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;

namespace CrudAppSample.Products.Service.Interfaces;

public interface IProductCommandService
{
    Task<Product> CreateProduct(CreateProductRequest productRequest);
}