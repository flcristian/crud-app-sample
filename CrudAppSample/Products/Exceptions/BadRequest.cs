namespace CrudAppSample.Products.Exceptions;

public class BadRequest : Exception
{
    public BadRequest(string? message) : base(message) { }
}