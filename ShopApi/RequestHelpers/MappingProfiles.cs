using AutoMapper;
using ShopApi.DTOs;
using ShopApi.Models;

namespace ShopApi.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EditProductDto, Product>();
        }
    }
}
