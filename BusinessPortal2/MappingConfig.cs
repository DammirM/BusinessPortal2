using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;

namespace BusinessPortal2
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Personal, PersonalReadDTO>();
            CreateMap<Personal, RegisterPersonalDTO>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeUpdateDTO>().ReverseMap();
            CreateMap<Personal, PersonalUpdateDTO>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestReadDTO>();
            CreateMap<LeaveRequest, LeaveRequestReadAdminDTO>();
            CreateMap<LeaveRequestCreateDTO, LeaveRequest>();
            CreateMap<LeaveRequestUpdateDTO, LeaveRequest>();
        }
    }
}
