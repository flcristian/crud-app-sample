using CrudAppSample.Products.Model;
using CrudAppSample.Products.Repository;
using CrudAppSample.Products.Service;
using CrudAppSample.System.Constants;
using CrudAppSample.System.Exceptions;
using Moq;
using tests.Products.Helpers;

namespace tests.Products;

public class ProductQueryServiceTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly ProductQueryService _service;

    public ProductQueryServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductQueryService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllProducts_ProductsExist_ReturnsAllProducts()
    {
        var products = TestProductFactory.CreateProducts(3);

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
        var result = await _service.GetAllProducts();
        
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        Assert.Contains(products[0], result);
        Assert.Contains(products[1], result);
    }
    
    [Fact]
    public async Task GetAllProducts_NoProductsExist_ThrowItemsDoNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());
        
        ItemsDoNotExist exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAllProducts());
        
        Assert.Equal(Constants.NO_PRODUCTS_EXIST, exception.Message);
    }
}