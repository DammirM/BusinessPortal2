using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;

namespace BusinessPortal2
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Personal, PersonalReadDTO>();
            CreateMap<RegisterPersonalDTO, Personal>();
            CreateMap<PersonalUpdateDTO, Personal>();

            CreateMap<LeaveType, LeaveTypeReadDTO>()
                .ForMember(destination => destination.leaveRequests, opt => opt.Ignore());
            CreateMap<LeaveType, LeaveTypeSimpleReadDTO>()
                .ForMember(destination => destination.leaveRequests, opt => opt.Ignore());
            CreateMap<LeaveTypeUpdateDTO, LeaveType>()
                .ForMember(destination => destination.leaveRequests, opt => opt.Ignore());
            CreateMap<LeaveTypeCreateDTO, LeaveType>()
                .ForMember(destination => destination.leaveRequests, opt => opt.Ignore());

            CreateMap<LeaveRequest, LeaveRequestReadDTO>();
            CreateMap<LeaveRequest, LeaveRequestReadAdminDTO>();
            CreateMap<LeaveRequestCreateDTO, LeaveRequest>();
            CreateMap<LeaveRequestUpdateDTO, LeaveRequest>().ReverseMap();
        }
    }
}
