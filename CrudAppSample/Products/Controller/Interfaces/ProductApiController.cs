using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using Microsoft.AspNetCore.Mvc;
    
namespace CrudAppSample.Products.Controller.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ProductApiController:ControllerBase
{
    [HttpGet("all")]
    [ProducesResponseType(statusCode:200,type:typeof(IEnumerable<Product>))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult<IEnumerable<Product>>> GetProducts();

    [HttpPost("create")]
    [ProducesResponseType(statusCode:201,type:typeof(Product))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    public abstract Task<ActionResult<Product>> CreateProduct([FromBody]CreateProductRequest productRequest);

    [HttpPut("update")]
    [ProducesResponseType(statusCode:202,type:typeof(Product))]
    [ProducesResponseType(statusCode:400,type:typeof(String))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult<Product>> UpdateProduct([FromBody]UpdateProductRequest productRequest);
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(statusCode:202,type:typeof(Product))]
    [ProducesResponseType(statusCode:404,type:typeof(String))]
    public abstract Task<ActionResult<Product>> DeleteProduct([FromRoute]int id);
}