using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands.CategoryCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Models;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class CategoryController : ControllerBase
{
    private readonly NitecoContext _context;

    public CategoryController(NitecoContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _context.Categories.Include(a => a.Products).ToListAsync();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return Ok(category);
    }
}