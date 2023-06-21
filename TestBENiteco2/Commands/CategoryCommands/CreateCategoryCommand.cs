using System.ComponentModel.DataAnnotations;

namespace TestBENiteco2.Commands.CategoryCommands;

public class CreateCategoryCommand
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}