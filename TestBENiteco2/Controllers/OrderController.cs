using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands;
using TestBENiteco2.Commands.OrderCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Enums;
using TestBENiteco2.Models;
using TestBENiteco2.Response;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class OrderController : ControllerBase
{
    private readonly NitecoContext _context;
    private readonly IMapper _mapper;

    public OrderController(NitecoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllOrders(string searchKey, string orderBy, SortedDirection order, int? pageNumber)
    {
        var query = _context.Orders
            .AsNoTracking();

        if (!String.IsNullOrEmpty(searchKey))
        {
            query = query.Where(s => s.Product.Category.Name.Contains(searchKey));
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            query = query.OrderByWithDynamic(orderBy, x => x.OrderDate, order);
        }

        var results = await PaginatedList<OrderResponse>.CreateAsync(query.ProjectTo<OrderResponse>(_mapper.ConfigurationProvider),
            pageNumber ?? 1, 6);
        return Ok(results);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderCommand item)
    {
        var itemProduct = _context.Products.FirstOrDefault(x => x.Id == Guid.Parse(item.ProductId));

        if (itemProduct == null || itemProduct.Quantity < item.Amount)
        {
            return BadRequest(new { Message = "So luong yeu cau vuot qua gioi han", Code = "ER_01" });
        }

        var contact = new Order
        {
            Id = Guid.NewGuid(),
            Name = item.Name,
            ProductId = Guid.Parse(item.ProductId),
            CustomerId = Guid.Parse(item.CustomerId),
            OrderDate = item.OrderDate,
            Amount = item.Amount
        };
        await _context.Orders.AddAsync(contact);
        itemProduct.Quantity -= item.Amount;
        await _context.SaveChangesAsync();
        return Ok(contact);
    }
}