using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Repository;
using CrudAppSample.Products.Service.Interfaces;
using CrudAppSample.System.Constants;
using CrudAppSample.System.Exceptions;
using Moq;
using tests.Products.Helpers;

namespace tests.Products;

public class ProductCommandServiceTests
{
    private readonly Mock<IProductRepository> _mockRepo;
    private readonly IProductCommandService _service;

    public ProductCommandServiceTests()
    {
        _mockRepo = new Mock<IProductRepository>();
        _service = new ProductCommandService(_mockRepo.Object);
    }
    
    [Fact]
    public async Task CreateProduct_InvalidPrice_ThrowsInvalidPriceException()
    {
        var createRequest = new CreateProductRequest
        {
            Name = "New Product",
            Price = -100,
            Category = "Test Category",
            DateOfFabrication = DateTime.UtcNow
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = createRequest.Name;
        expectedProduct.Price = createRequest.Price;

        _mockRepo.Setup(repo => repo.GetByNameAsync(createRequest.Name)).ReturnsAsync((Product)null);
        _mockRepo.Setup(repo => repo.CreateAsync(createRequest)).ReturnsAsync(expectedProduct);

        var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _service.CreateProduct(createRequest));
        
        Assert.Equal(Constants.INVALID_PRICE, exception.Message);
    }
    
    [Fact]
    public async Task CreateProduct_ProductWithSameNameAlreadyExists_ThrowsItemAlreadyExistsException()
    {
        var createRequest = new CreateProductRequest
        {
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            DateOfFabrication = DateTime.UtcNow
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = createRequest.Name;
        expectedProduct.Price = createRequest.Price;

        var existingProduct = TestProductFactory.CreateProduct(2);
        existingProduct.Name = createRequest.Name;

        _mockRepo.Setup(repo => repo.GetByNameAsync(createRequest.Name)).ReturnsAsync(existingProduct);
        _mockRepo.Setup(repo => repo.CreateAsync(createRequest)).ReturnsAsync(expectedProduct);

        var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateProduct(createRequest));
        
        Assert.Equal(Constants.PRODUCT_ALREADY_EXISTS, exception.Message);
    }
    
    [Fact]
    public async Task CreateProduct_ValidData_ReturnsCreatedProduct()
    {
        var createRequest = new CreateProductRequest
        {
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            DateOfFabrication = DateTime.UtcNow
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = createRequest.Name;

        _mockRepo.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Product)null);
        _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CreateProductRequest>())).ReturnsAsync(expectedProduct);

        var result = await _service.CreateProduct(createRequest);

        Assert.NotNull(result);
        Assert.Equal(createRequest.Name, result.Name);
    }
}