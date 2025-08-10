using AutoMapper;

namespace EmployeeApp
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Source -> Target
            CreateMap<Models.Employee, Models.DTO.EmployeeDTO>().ReverseMap();
            CreateMap<Models.Employee, Models.DTO.EmployeeCreateDTO>().ReverseMap();
            CreateMap<Models.Employee, Models.DTO.EmployeeUpdateDTO>().ReverseMap();
        }
    }
}
