using System.ComponentModel.DataAnnotations;

namespace TestBENiteco2.Commands.OrderCommands;

public class CreateOrderCommand
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string ProductId { get; set; }
    [Required]
    public string CustomerId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public int Amount { get; set; }
}