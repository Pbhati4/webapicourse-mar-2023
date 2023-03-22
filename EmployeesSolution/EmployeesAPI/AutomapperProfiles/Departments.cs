using AutoMapper;
using EmployeesApi.Models;

namespace EmployeesAPI.AutomapperProfiles;


    public class Departments : Profile
    {
        public Departments()
        {
        //CreateMap<DepartmentEntity, DepartmentItem>();
        CreateMap<DepartmentEntity, DepartmentItem>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Code));
    }
    }
