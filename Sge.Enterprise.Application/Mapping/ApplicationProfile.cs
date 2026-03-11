namespace Sge.Enterprise.Application.Mapping;
using AutoMapper;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Application.Dtos;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        
        CreateMap<EmployeeAddDto, Employee>();
        CreateMap<EmployeeUpdateDto, Employee>();

        CreateMap<Area, AreaDto>();
        CreateMap<AreaAddDto, Area>();
        CreateMap<AreaUpdateDto, Area>();

        CreateMap<RegisterDto, User>()
            .ReverseMap();
    }
}