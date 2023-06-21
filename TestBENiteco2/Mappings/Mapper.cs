using AutoMapper;
using TestBENiteco2.Models;
using TestBENiteco2.Response;

namespace TestBENiteco2.Mappings;

public sealed class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(a => a.CategoryName, opt => opt.MapFrom(a => a.Category.Name));

        CreateMap<Order, OrderResponse>()
            .ForMember(a => a.CustomerName, opt => opt.MapFrom(a => a.Customer.Name))
            .ForMember(a => a.ProductName, opt => opt.MapFrom(a => a.Product.Name))
            .ForMember(a => a.ProductPrice, opt => opt.MapFrom(a => a.Product.Price));

        CreateMap<Category, CategoryResponse>();
        
        CreateMap<Customer, CustomerResponse>();
    }
}