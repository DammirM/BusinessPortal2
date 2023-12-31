﻿using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;

namespace WebApplicationBusinessPortal2.Services
{
    public interface ILeaveRequestAdminService
    {
        // LeaveRequest
        Task<T> GetLeaveRequestAdminAsync<T>();
        Task<T> GetPersonalLeaveRequestAdminAsync<T>(int Id);
        Task<T> GetLeaveRequestByIdAdminAsync<T>(int Id); 
        Task<T> CreateLeaveRequestAdminAsync<T>(LeaveRequestCreateDTO leaveRequest);
        Task<T> UpdateLeaveRequesAdminAsync<T>(LeaveRequestUpdateDTO leaveRequest);
        Task<T> DeleteLeaveRequestAdminAsync<T>(int Id);
        Task<T> ExportDataAdminAsync<T>(DateTime start, DateTime end);

        // LeaveType 

        Task<T> GetLeaveTypeAsync<T>();

        Task<T> GetLeaveTypeByIdtAdminAsync<T>(int Id);

        Task<T> UpdateLeaveTypeAdminAsync<T>(LeaveTypeUpdateDTO leaveDTO);

        Task<T> CreateLeveType<T>(LeaveTypeCreateDTO leaveDTO);

        Task<T> DeleteLeaveTypeByNameAsync<T>(string name);
        Task<T> GetTotalDaysApprovedFromAllLeaveTypes<T>();

        // Personal

        Task<T> GetPersonalAdminAsync<T>();

        Task<T> GetPersonalByIdAdminAsync<T>(int Id);

        Task<T> GetLeaveTypeByPersonalId<T>(int Id);

        Task<T> UpdatePersonalAdminAsync<T>(PersonalUpdateDTO leaveRequest);

        Task<T> DeletePersonalAsync<T>(int Id);
    }
}
