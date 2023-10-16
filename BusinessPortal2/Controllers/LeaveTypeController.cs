using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessPortal2.Models;
using BusinessPortal2.Services;

namespace BusinessPortal2.Controllers
{
    [Route("api/leavetypes")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeController(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
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
        public async Task<IActionResult> UpdateLeaveType(int id, LeaveType leaveType)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };


            var updatedLeaveType = await _leaveTypeRepository.UpdateLeave(id, leaveType);
                if (updatedLeaveType != null)
                {
                response.body= updatedLeaveType;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(updatedLeaveType);
                }
            return NotFound("PersonalId Not Found");

        }
    }
}
