using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands.ProductCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Models;
using TestBENiteco2.Response;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ProductController : ControllerBase
{
    private readonly NitecoContext _context;
    private readonly IMapper _mapper;

    public ProductController(NitecoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result = await _context.Products
            .ProjectTo<ProductResponse>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = Guid.Parse(command.CategoryId),
            Price = command.Price,
            Description = command.Description,
            Quantity = command.Quantity,
            Name = command.Name,
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return Ok(product);
    }
}