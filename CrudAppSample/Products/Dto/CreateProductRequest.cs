namespace CrudAppSample.Products.Dto;

public class CreateProductRequest
{
    
    public double Price { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public DateTime DateOfFabrication { get; set; }
}