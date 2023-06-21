using TestBENiteco2.Models;

namespace TestBENiteco2.Response;

public sealed class OrderResponse : ModelResponse
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string ProductName { get; set; }
    public string ProductPrice { get; set; }
    public string ProductId { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public DateTime OrderDate { get; set; }
}