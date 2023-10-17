using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO;
using BusinessPortal2.Models.DTO.PersonalDTO;

namespace BusinessPortal2
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Personal, RegisterPersonalDTO>().ReverseMap();
            CreateMap<LeaveType, UpdateLeaveDTO>().ReverseMap();
            CreateMap<Personal, PersonalUpdateDTO>().ReverseMap();
        }
    }
}
