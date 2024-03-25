using AutoMapper;
using WebApplication1.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.Heplers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserVM, User>();
        }

    }
}
