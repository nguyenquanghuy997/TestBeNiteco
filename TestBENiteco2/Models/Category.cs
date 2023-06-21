namespace TestBENiteco2.Models;

public class Category : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> Products { get; set; }
}