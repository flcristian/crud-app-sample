using System.Net;
using System.Text;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using Newtonsoft.Json;
using tests.Infrastructure;

namespace tests.Products.IntegrationTests;

public class ProductIntegrationTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_GetAll_ProductsFound_ReturnsOkStatusCode_ValidProductsContentResponse()
    {
        
        var request = "/api/v1/product/create";
        var product = new CreateProductRequest { Name = "New Product 1", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/product/create";
        var product = new CreateProductRequest { Name = "New Product 1", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(request, content);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Product>(responseString);
        
        Assert.NotNull(result);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Price, result.Price);
        Assert.Equal(product.Category, result.Category);
        Assert.Equal(product.DateOfFabrication, result.DateOfFabrication);
    }
    
    [Fact]
    public async Task Post_Create_ProductAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/product/create";
        var product = new CreateProductRequest { Name = "New Product", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
        
        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_Create_InvalidProductPrice_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/product/create";
        var product = new CreateProductRequest { Name = "Test Product", Price = -9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/product/create";
        var createProduct = new CreateProductRequest { Name = "New Product 4", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Product>(responseString)!;
        
        request = "/api/v1/product/update";
        var updateProduct = new UpdateProductRequest { Id = result.Id, Name = "New Product 3", Price = 1.49, Category = "Category", DateOfFabrication = DateTime.Now };
        content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        
        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Product>(responseString)!;
        
        Assert.Equal(updateProduct.Name, result.Name);
        Assert.Equal(updateProduct.Price, result.Price);
        Assert.Equal(updateProduct.Category, result.Category);
        Assert.Equal(updateProduct.DateOfFabrication, result.DateOfFabrication);
    }

    [Fact]
    public async Task Put_Update_InvalidProductPrice_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/product/create";
        var createProduct = new CreateProductRequest { Name = "New Product 2", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Product>(responseString)!;
        
        request = "/api/v1/product/update";
        var updateProduct = new UpdateProductRequest { Id = result.Id, Name = "New Product 3", Price = -1.49, Category = "Category", DateOfFabrication = DateTime.Now };
        content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");
        
        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_Update_ProductDoesNotExist_ReturnsNotFoundStatusCode()
    {
        var request = "/api/v1/product/update";
        var updateProduct = new UpdateProductRequest { Id = 99999, Name = "New Product 3", Price = 1.49, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(updateProduct), Encoding.UTF8, "application/json");
        
        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Delete_ProductExists_ReturnsDeletedProduct()
    {
        var request = "/api/v1/product/create";
        var createProduct = new CreateProductRequest { Name = "New Product 5", Price = 9.99, Category = "Category", DateOfFabrication = DateTime.Now };
        var content = new StringContent(JsonConvert.SerializeObject(createProduct), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Product>(responseString)!;
        
        request = $"/api/v1/product/delete/{result.Id}";

        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Delete_ProductDoesNotExist_ReturnsNotFoundStatusCode()
    {
        var request = "/api/v1/product/delete/77777";
        
        var response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}