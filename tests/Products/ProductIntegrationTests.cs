using System.Net;
using System.Text;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using Newtonsoft.Json;
using tests.Infrastructure;

namespace tests.Products;

public class ProductIntegrationTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Product_ReturnsSuccessStatusCode()
    {
        var request = "/api/v1/product/create";
        var product = new CreateProductRequest { Name = "dsja Product", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(request, content);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}