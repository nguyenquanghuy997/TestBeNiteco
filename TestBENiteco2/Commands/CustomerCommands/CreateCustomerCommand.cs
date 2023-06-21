using System.ComponentModel.DataAnnotations;

namespace TestBENiteco2.Commands.CustomerCommands;

public class CreateCustomerCommand
{
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
}