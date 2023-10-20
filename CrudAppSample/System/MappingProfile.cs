using AutoMapper;
using CrudAppSample.Products.Dto;
using CrudAppSample.Products.Model;

namespace CrudAppSample.System;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductRequest, Product>();
    }
}