namespace TestBENiteco2.Response;

public sealed class CategoryResponse : ModelResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ProductResponse> Products { get; set; }
}