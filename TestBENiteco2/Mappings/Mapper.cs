using AutoMapper;
using TestBENiteco2.Models;
using TestBENiteco2.Response;

namespace TestBENiteco2.Mappings;

public sealed class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(a => a.CategoryName, opt => opt.MapFrom(a => a.Category.Name))
            .ForMember(a => a.CategoryId, opt => opt.MapFrom(a => a.Category.Id));
    }
}