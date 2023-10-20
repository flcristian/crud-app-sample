using CrudAppSample.Products.Controller.Interfaces;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Exceptions;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Service.Interfaces;
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
        catch (BadRequest badRequest)
        {
            return BadRequest(badRequest.Message);
        }
    }

    public override async Task<ActionResult<Product>> CreateProduct(CreateProductRequest productRequest)
    {
        try
        {
            var product = await _productCommandService.CreateProduct(productRequest);

            return Ok(product);
        }
        catch (BadRequest badRequest)
        {
            return BadRequest(badRequest.Message);
        }
    }
}