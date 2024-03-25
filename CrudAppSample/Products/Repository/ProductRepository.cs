using AutoMapper;
using CrudAppSample.Data;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;
using Microsoft.EntityFrameworkCore;
namespace CrudAppSample.Products.Repository;

public class ProductRepository:IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByNameAsync(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(product => product.Name.Equals(name));
    }

    public async Task<Product> CreateAsync(CreateProductRequest productRequest)
    {
        var product = _mapper.Map<Product>(productRequest);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    
    public async Task<Product> UpdateAsync(UpdateProductRequest productRequest)
    {
        var product = (await _context.Products.FindAsync(productRequest.Id))!;

        product.Price = productRequest.Price ?? product.Price;
        product.Name = productRequest.Name ?? product.Name;
        product.Category = productRequest.Category ?? product.Category;
        product.DateOfFabrication = productRequest.DateOfFabrication ?? product.DateOfFabrication;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}