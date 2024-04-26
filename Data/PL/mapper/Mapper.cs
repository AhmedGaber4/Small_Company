using AutoMapper;
using Data;
using PL.Models;

namespace PL.mapper
{
    public class HolaMapper : Profile
    {
        public HolaMapper()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
            CreateMap<DepartmentViewModel,Department>().ReverseMap();

        }
    }
}
