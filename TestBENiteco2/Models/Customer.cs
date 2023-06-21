namespace TestBENiteco2.Models;

public class Customer : BaseModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Order> Orders { get; set; }
}