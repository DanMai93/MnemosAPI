using AutoMapper;
using MnemosAPI.Models;
using MnemosAPI.DTO;
using MnemosAPI.DTO.AddRequestDto;
using MnemosAPI.DTO.UpdateRequestDto;
using MnemosAPI.DTO.FiltersDTO;

namespace MnemosAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Area, AreaDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills));

            CreateMap<Customer, CustomerDto>().ReverseMap();
                //.ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects));
            CreateMap<CustomerGroupDto, Customer>().ReverseMap();

            CreateMap<EndCustomer, EndCustomerDto>().ReverseMap();
      
            CreateMap<EndCustomer, EndCustomerGroupDto>().ReverseMap();

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<AddProjectRequestDto, Project>().ReverseMap();
            CreateMap<UpdateProjectRequestDto, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectFilterDto>().ReverseMap();
            CreateMap<UpdateProjectRequestDto, Project>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<RoleGroupDto, Role>().ReverseMap();

            CreateMap<Scale, ScaleDto>().ReverseMap()
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills));

            CreateMap<Sector, SectorDto>().ReverseMap();
            CreateMap<Sector, SectorGroupDto>().ReverseMap()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects));

            CreateMap<Skill, SkillDto>().ReverseMap()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Areas))
            .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.Scales));

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<BusinessUnit, BusinessUnitDto>().ReverseMap();

            CreateMap<Architecture, ArchitectureDto>().ReverseMap();

            CreateMap<WorkMethod, WorkMethodDto>().ReverseMap();

            CreateMap<ManagementTool, ManagementToolDto>().ReverseMap();

            CreateMap<SoftSkill, SoftSkillDto>().ReverseMap();

        }
    }
}
