﻿using Api.ViewModel;
using AutoMapper;
using Business.Model;

namespace Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Sale, SaleViewModel>().ReverseMap();
            CreateMap<ProductSale, ProductSaleViewModel>().ReverseMap();
        }
    }
}
