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
            try
            {
                var leaveTypes = await _leaveTypeRepository.GetAll();
                return Ok(leaveTypes);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveTypeById(int id)
        {
            try
            {
                var leaveType = await _leaveTypeRepository.GetById(id);
                if (leaveType != null)
                {
                    return Ok(leaveType);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpDelete("{personalId}")]
        public async Task<IActionResult> DeleteLeaveType(int personalId)
        {
            try
            {
                var deletedLeaveType = await _leaveTypeRepository.DeleteLeave(personalId);
                if (deletedLeaveType != null)
                {
                    return Ok(deletedLeaveType);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveType(int id, LeaveType leaveType)
        {
            try
            {
                var updatedLeaveType = await _leaveTypeRepository.UpdateLeave(id, leaveType);
                if (updatedLeaveType != null)
                {
                    return Ok(updatedLeaveType);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}
