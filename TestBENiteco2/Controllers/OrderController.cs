using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.Commands;
using TestBENiteco2.Commands.OrderCommands;
using TestBENiteco2.DataContext;
using TestBENiteco2.Enums;
using TestBENiteco2.Models;

namespace TestBENiteco2.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public sealed class OrderController : ControllerBase
{
    private readonly NitecoContext _context;

    public OrderController(NitecoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetOrders(string searchKey, string orderBy, SortedDirection order, int? pageNumber)
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

        var results = await PaginatedList<Order>.CreateAsync(query.Include(a => a.Product).Include(a => a.Customer),
            pageNumber ?? 1, 6);
        return Ok(results);
    }

    [HttpPost]
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