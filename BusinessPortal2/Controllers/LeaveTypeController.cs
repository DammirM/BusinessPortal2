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

            var leaves = await _leaveTypeRepository.GetAll();
            if (leaves.Any())
            {
                response.body= leaves;
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

            var leave = await _leaveTypeRepository.GetById(leaveTypeId);
            if (leave != null)
            {
                response.body= leave;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            return BadRequest("Personal Not sfjhdsfk");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateLeaveType(LeaveTypeUpdateDTO leaveTypeUpdateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (leaveTypeUpdateDTO != null)
            {
                await _leaveTypeRepository.UpdateLeave(_mapper.Map<LeaveType>(leaveTypeUpdateDTO));
                response.body = leaveTypeUpdateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
