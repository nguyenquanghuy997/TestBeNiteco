namespace TestBENiteco2.Response;

public sealed class OrderItem : ModelResponse
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public DateTime OrderDate { get; set; }
    public int Amount { get; set; }
    public string Name { get; set; }
}