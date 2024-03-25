namespace CrudAppSample.Products.Model.Comparers;

public class ProductEqualityComparer : IEqualityComparer<Product>
{
    public bool Equals(Product x, Product y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Price.Equals(y.Price) && x.Name == y.Name && x.Category == y.Category && x.DateOfFabrication.Equals(y.DateOfFabrication);
    }

    public int GetHashCode(Product obj)
    {
        return HashCode.Combine(obj.Id, obj.Price, obj.Name, obj.Category, obj.DateOfFabrication);
    }
}