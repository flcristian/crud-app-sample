using CrudAppSample.Products.Controller.Interfaces;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Service.Interfaces;
using CrudAppSample.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CrudAppSample.Products.Controller;

public class ProductController : ProductApiController
{
    private IProductQueryService _productQueryService;
    private IProductCommandService _productCommandService;

    public ProductController(IProductQueryService productQueryService, IProductCommandService productCommandService)
    {
        _productQueryService = productQueryService;
        _productCommandService = productCommandService;
    }

    public override async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            var products = await _productQueryService.GetAllProducts();

            return Ok(products);
        }
        catch (ItemsDoNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }

    public override async Task<ActionResult<Product>> CreateProduct(CreateProductRequest productRequest)
    {
        try
        {
            var product = await _productCommandService.CreateProduct(productRequest);

            return Ok(product);
        }
        catch (InvalidPrice ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public override async Task<ActionResult<Product>> UpdateProduct(UpdateProductRequest productRequest)
    {
        try
        {
            var product = await _productCommandService.UpdateProduct(productRequest);

            return Ok(product);
        }
        catch (InvalidPrice ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ItemDoesNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    public override async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        try
        {
            Product product = await _productCommandService.DeleteProduct(id);

            return Ok(product);
        }
        catch (ItemDoesNotExist ex)
        {
            return NotFound(ex.Message);
        }
    }
}