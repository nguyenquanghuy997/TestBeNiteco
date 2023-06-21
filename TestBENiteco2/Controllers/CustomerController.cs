using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands.CustomerCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Models;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class CustomerController : ControllerBase
{
    private readonly NitecoContext _context;

    public CustomerController(NitecoContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var result = await _context.Customers.Include(a => a.Orders).ToListAsync();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Address = command.Address,
        };
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return Ok(customer);
    }
}