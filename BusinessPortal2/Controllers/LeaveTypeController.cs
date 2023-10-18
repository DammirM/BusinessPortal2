using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessPortal2.Models;
using BusinessPortal2.Services;
using AutoMapper;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;

namespace BusinessPortal2.Controllers
{
    [Route("api/leavetypes")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeRepo _leaveTypeRepository;
        private readonly IMapper _mapper;

        public LeaveTypeController(ILeaveTypeRepo leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllLeaveTypes()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leaveTypeAll = await _leaveTypeRepository.GetAllLeaveType();
            if (leaveTypeAll.Any())
            {
                response.body= _mapper.Map<LeaveTypeReadDTO>(leaveTypeAll);
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
                response.body= _mapper.Map<LeaveTypeReadDTO>(leaveTypeById);
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
                response.body = leaveTypeCreateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
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
                response.body = leaveTypeUpdateDTO;
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
    }
}
