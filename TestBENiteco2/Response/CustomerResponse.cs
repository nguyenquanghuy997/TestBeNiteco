namespace TestBENiteco2.Response;

public sealed class CustomerResponse : ModelResponse
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<OrderResponse> Orders { get; set; }
}