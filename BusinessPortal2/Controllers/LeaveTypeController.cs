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
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpGet("{personalId}")]
        public async Task<IActionResult> GetLeaveTypeById(int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var leave = await _leaveTypeRepository.GetById(id);
            if (leave != null)
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }

            return NotFound(response);
        }

        [HttpDelete("{personalId}")]
        public async Task<IActionResult> DeleteLeaveType(int personalId)
        {

            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            
                var deletedLeaveType = await _leaveTypeRepository.DeleteLeave(personalId);
                if (deletedLeaveType != null)
                {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(deletedLeaveType);
                }
                
                return NotFound("PersonalId Not Found");
            
        }

        [HttpPut("{personalId}")]
        public async Task<IActionResult> UpdateLeaveType(int id, LeaveType leaveType)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };


            var updatedLeaveType = await _leaveTypeRepository.UpdateLeave(id, leaveType);
                if (updatedLeaveType != null)
                {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(updatedLeaveType);
                }
            return NotFound("PersonalId Not Found");

        }
    }
}
