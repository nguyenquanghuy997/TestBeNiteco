namespace TestBENiteco2.Response;

public sealed class ProductResponse : ModelResponse
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public string CategoryName { get; set; }
    public string CategoryId { get; set; }
}