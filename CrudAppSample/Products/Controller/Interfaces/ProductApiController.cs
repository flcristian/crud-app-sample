using System.Runtime.InteropServices.JavaScript;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using Microsoft.AspNetCore.Mvc;

namespace CrudAppSample.Products.Controller.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ProductApiController:ControllerBase
{
    [HttpGet("all")]
    public abstract Task<ActionResult<IEnumerable<Product>>> GetProducts();

    [HttpPost("create")]
    [ProducesResponseType(statusCode:200,type:typeof(Product))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult<Product>> CreateProduct(CreateProductRequest productRequest);
}