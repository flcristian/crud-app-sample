using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using CrudAppSample.Products.Model.Comparers;
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

        _mockRepo.Setup(repo => repo.GetByNameAsync(createRequest.Name)).ReturnsAsync((Product)null!);

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

        _mockRepo.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Product)null!);
        _mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CreateProductRequest>())).ReturnsAsync(expectedProduct);

        var result = await _service.CreateProduct(createRequest);

        Assert.NotNull(result);
        Assert.Equal(createRequest.Name, result.Name);
    }
    
    [Fact]
    public async Task UpdateProduct_InvalidPrice_ThrowsInvalidPriceException()
    {
        var updateRequest = new UpdateProductRequest
        {
            Id = 1,
            Name = "New Product",
            Price = -100,
            Category = "Test Category",
            DateOfFabrication = DateTime.UtcNow
        };

        var unmodifiedProduct = TestProductFactory.CreateProduct(1);
        
        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Price = -100;

        _mockRepo.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync(unmodifiedProduct);

        var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _service.UpdateProduct(updateRequest));
        
        Assert.Equal(Constants.INVALID_PRICE, exception.Message);
    }
    
    [Fact]
    public async Task UpdateProduct_ProductDoesNotExist_ThrowsItemDoesNotExistException()
    {
        var updateRequest = new UpdateProductRequest
        {
            Id = 1,
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            DateOfFabrication = DateTime.UtcNow
        };

        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Price = 100;

        _mockRepo.Setup(repo => repo.GetByIdAsync(updateRequest.Id)).ReturnsAsync((Product)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateProduct(updateRequest));
        
        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task UpdateProduct_ValidData_ReturnsUpdatedProduct()
    {
        DateTime time = DateTime.Now;
        var updateRequest = new UpdateProductRequest
        {
            Id = 1,
            Name = "New Product",
            Price = 100,
            Category = "Test Category",
            DateOfFabrication = time
        };

        var unmodifiedProduct = TestProductFactory.CreateProduct(1);
        
        var expectedProduct = TestProductFactory.CreateProduct(1);
        expectedProduct.Name = updateRequest.Name;
        expectedProduct.Price = 100;
        expectedProduct.Category = "Test Category";
        expectedProduct.DateOfFabrication = time;

        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(unmodifiedProduct);
        _mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<UpdateProductRequest>())).ReturnsAsync(expectedProduct);

        var result = await _service.UpdateProduct(updateRequest);

        Assert.NotNull(result);
        Assert.Equal(expectedProduct, result, new ProductEqualityComparer());
    }
    
    [Fact]
    public async Task DeleteProduct_ProductDoesNotExist_ThrowsItemDoesNotExistException()
    {
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null!);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteProduct(1));
        
        Assert.Equal(Constants.PRODUCT_DOES_NOT_EXIST, exception.Message);
    }
    
    [Fact]
    public async Task DeleteProduct_ValidData_ReturnsUpdatedProduct()
    {
        var product = TestProductFactory.CreateProduct(1);
        
        _mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

        var result = await _service.DeleteProduct(1);

        Assert.NotNull(result);
        Assert.Equal(product, result, new ProductEqualityComparer());
    }
}