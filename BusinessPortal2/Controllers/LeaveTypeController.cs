using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessPortal2.Models;
using BusinessPortal2.Services;
using AutoMapper;
using BusinessPortal2.Models.DTO;

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

        [HttpGet]
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

            return NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveTypeById(int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leave = await _leaveTypeRepository.GetById(id);
            if (leave != null)
            {
                response.body= leave;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }

            return NotFound("Personal Not sfjhdsfk");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveType(int id, UpdateLeaveDTO updateLeaveDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leaveTypeToUpdate = _mapper.Map<LeaveType>(updateLeaveDTO);
            leaveTypeToUpdate.PersonalId = id;

            var updatedLeaveType = await _leaveTypeRepository.UpdateLeave(id, leaveTypeToUpdate);

            if (updatedLeaveType != null)
            {
                response.body = updatedLeaveType;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(response);
            }

            return NotFound("PersonalId Not Found");
        }
    }
}
