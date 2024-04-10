using AutoMapper;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Heplers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserVM, User>();
        }

    }
}
