using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO;

namespace BusinessPortal2
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Personal, RegisterPersonalDTO>().ReverseMap();
            CreateMap<LeaveType, UpdateLeaveDTO>().ReverseMap();
        }
    }
}
