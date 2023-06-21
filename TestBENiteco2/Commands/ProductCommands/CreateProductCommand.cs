using System.ComponentModel.DataAnnotations;

namespace TestBENiteco2.Commands.ProductCommands;

public class CreateProductCommand
{
    [Required]
    public string Name { set; get; }
    [Required]
    public string CategoryId { get; set; }
    [Required]
    public int Price { get; set; }
    public string Description { get; set; }
    [Required]
    public int Quantity { get; set; }
}