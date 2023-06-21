using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands.CustomerCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Models;
using TestBENiteco2.Response;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class CustomerController : ControllerBase
{
    private readonly NitecoContext _context;
    private readonly IMapper _mapper;

    public CustomerController(NitecoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCustomers()
    {
        var result = await _context.Customers.ProjectTo<CustomerResponse>(_mapper.ConfigurationProvider).ToListAsync();
        return Ok(result);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
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