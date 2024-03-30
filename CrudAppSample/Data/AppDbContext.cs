using CrudAppSample.Products.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudAppSample.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Product> Products { get; set; }

    
}