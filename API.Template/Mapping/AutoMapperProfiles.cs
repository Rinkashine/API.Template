using AutoMapper;
using Template.Domain.DTO;
using Template.Domain.Entities;

namespace API.Template.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Category
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<AddCategoryRequestDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();

        }
       
    }
}
