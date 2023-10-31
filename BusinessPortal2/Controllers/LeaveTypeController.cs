using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessPortal2.Models;
using BusinessPortal2.Services;
using AutoMapper;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using Microsoft.AspNetCore.Authorization;

namespace BusinessPortal2.Controllers
{
    [Route("api/leavetypes")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeRepo _leaveTypeRepository;
        private readonly LeaveRequestAdminRepo _leaveRequestRepo;
        private readonly IMapper _mapper;

        public LeaveTypeController(ILeaveTypeRepo leaveTypeRepository, IMapper mapper, LeaveRequestAdminRepo leaveRequestRepo)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _leaveRequestRepo = leaveRequestRepo;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllLeaveTypes()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leaveTypeAll = await _leaveTypeRepository.GetAllLeaveType();
            if (leaveTypeAll.Any())
            {
                response.Result = _mapper.Map<IEnumerable<LeaveTypeReadDTO>>(leaveTypeAll);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("get/{leaveTypeId}")]
        public async Task<IActionResult> GetLeaveTypeById(int leaveTypeId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leaveTypeById = await _leaveTypeRepository.GetLeaveTypeById(leaveTypeId);
            if (leaveTypeById != null)
            {
                response.Result = _mapper.Map<LeaveTypeReadDTO>(leaveTypeById);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("get/All/{personalId}")]
        public async Task<IActionResult> GetAllLeaveByPersonalId(int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leaveTypeById = await _leaveTypeRepository.GetAllLeaveTypeByPersonalId(personalId);
            if (leaveTypeById != null)
            {
                response.Result = _mapper.Map<IEnumerable<LeaveTypeReadDTO>>(leaveTypeById);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLeaveType(LeaveTypeCreateDTO leaveTypeCreateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (leaveTypeCreateDTO != null)
            {
                await _leaveTypeRepository.CreateLeaveType(_mapper.Map<LeaveType>(leaveTypeCreateDTO));
                response.Result = leaveTypeCreateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Created("Created", response);
            }
            response.Errors.Add("LeaveTypeCreateDTO is null");
            return BadRequest(response);
        }

        [HttpPost("create/forall")]
        public async Task<IActionResult> CreateLeaveTypeToAll(LeaveTypeCreateDTO leaveTypeCreateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (leaveTypeCreateDTO != null)
            {
                await _leaveTypeRepository.CreateLeaveTypeForAll(_mapper.Map<LeaveType>(leaveTypeCreateDTO));
                response.Result = leaveTypeCreateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Created("Created", response);
            }
            response.Errors.Add("LeaveTypeCreateDTO is null");
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateLeaveType(LeaveTypeUpdateDTO leaveTypeUpdateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (leaveTypeUpdateDTO != null)
            {
                await _leaveTypeRepository.UpdateLeaveType(_mapper.Map<LeaveType>(leaveTypeUpdateDTO));
                response.Result = leaveTypeUpdateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }
            response.Errors.Add("LeaveTypeUpdateDTO is null");
            return BadRequest(response);
        }

        [HttpPut("update/leavename")]
        public async Task<IActionResult> UpdateLeaveTypeByName(LeaveTypeUpdateDTO leaveTypeUpdateDTO, string name)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (leaveTypeUpdateDTO != null)
            {
                await _leaveTypeRepository.UpdateLeaveByNameType(_mapper.Map<LeaveType>(leaveTypeUpdateDTO), name);
                response.Result = leaveTypeUpdateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }
            response.Errors.Add("LeaveTypeUpdateDTO is null");

            return BadRequest(response);
        }

        [HttpDelete("delete/{leaveTypeId}")]
        public async Task<IActionResult> DeleteLeaveType(int leaveTypeId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var leaveTypeToDelete = await _leaveTypeRepository.GetLeaveTypeById(leaveTypeId);
            if (leaveTypeToDelete != null)
            {
                await _leaveTypeRepository.DeleteLeaveType(leaveTypeId);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }
            response.Errors.Add($"LeaveType with id=[{leaveTypeId}] could not be found");
            return NotFound(response);
        }

        [HttpDelete("deleteByName/{name}")]
        public async Task<IActionResult> DeleteLeaveTypeByName(string name)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (name != null)
            {
                await _leaveTypeRepository.DeleteLeaveTypeByName(name);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }
            response.Errors.Add($"LeaveName [{name}] could not be found");
            return NotFound(response);
        }

        [HttpGet("total/leavetime/hours")]
        public async Task<IActionResult> GetTotalUsedLeaveTime()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            List<LeaveTypeTotalTime> leaveDays = new List<LeaveTypeTotalTime>();   
            List<string> leaveNameFind = new List<string>();
            int daysCount = 0;
            var leaveType = await _leaveRequestRepo.GetAll();
            if (leaveType.Any())
            {
                var leaves = leaveType.Where(status => status.ApprovalState == "Approved");
                if(leaves.Any())
                {
                    var uniqueLeaveNames = leaves.Select(l => l.leaveType.LeaveName.ToLower()).Distinct();
                    foreach (var uniqueName in uniqueLeaveNames)
                    {
                        var leaveName = uniqueName; 
                        foreach (var item in leaves.Where(l => l.leaveType.LeaveName.ToLower() == leaveName))
                        {
                            TimeSpan totalDays = item.EndDate - item.StartDate;
                            daysCount += totalDays.Days;
                        }
                        LeaveTypeTotalTime days = new LeaveTypeTotalTime
                        {
                            Name = leaveName,
                            Days = daysCount         
                        };
                        leaveDays.Add(days);
                        daysCount = 0;
                    }
                }
            }
            if (leaveDays.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Result = leaveDays;
                return Ok(response);
            }
            else
            {
                response.Errors.Add("Not able to get all the LeaveTypes total time data!");
                return BadRequest(response);
            }
        }
    }
}
