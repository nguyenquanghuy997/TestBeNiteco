namespace TestBENiteco2.Models;

public class Product : BaseModel
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}