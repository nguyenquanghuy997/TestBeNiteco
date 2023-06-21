namespace TestBENiteco2.Models;

public class Order : BaseModel
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public DateTime OrderDate { get; set; }
}