using CrudAppSample.Products.Model;

namespace tests.Products.Helpers;

public class TestProductFactory
{
    public static List<Product> CreateProducts(int count)
    {
        var products = new List<Product>();
        for (int i = 1; i <= count; i++)
        {
            products.Add(CreateProduct(i));
        }
        return products;
    }

    public static Product CreateProduct(int id)
    {
        return new Product
        {
            Id = id,
            Name = $"Product {id}",
            Price = 100 + 10 * id,
            Category = $"Category {id % 3 + 1}",
            DateOfFabrication = DateTime.UtcNow.AddDays(id)
        };
    }

    public static List<Product> CreateProductsInCategory(string category, int count)
    {
        var products = new List<Product>();
        for (int i = 1; i <= count; i++)
        {
            var product = CreateProduct(i);
            product.Category = category;
            products.Add(product);
        }
        return products;
    }

    public static List<Product> CreateProductsInPriceRange(double minPrice, double maxPrice, int count)
    {
        var products = new List<Product>();
        double priceIncrement = (maxPrice - minPrice) / count;
        for (int i = 0; i < count; i++)
        {
            var product = CreateProduct(i + 1);
            product.Price = minPrice + priceIncrement * i;
            products.Add(product);
        }
        return products;
    }

    public static Product CreateProductWithNoCategory(int id)
    {
        var product = CreateProduct(id);
        product.Category = null;
        return product;
    }

}