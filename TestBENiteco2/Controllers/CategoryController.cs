using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands.CategoryCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Models;
using TestBENiteco2.Response;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class CategoryController : ControllerBase
{
    private readonly NitecoContext _context;
    private readonly IMapper _mapper;

    public CategoryController(NitecoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _context.Categories.ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
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