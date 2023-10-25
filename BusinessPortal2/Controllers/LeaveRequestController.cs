using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal2.Controllers
{
    [Route("api/user/leaverequest")]
    [ApiController]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepo _leaveRequestRepo;
        private readonly IMapper _mapper;

        public LeaveRequestController(ILeaveRequestRepo leaveRequestRepo, IMapper mapper)
        {
            _leaveRequestRepo = leaveRequestRepo;
            _mapper = mapper;
        }

        [HttpGet("getall/{personalId}")]
        public async Task<IActionResult> GetAll(int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allLeaveRequests = await _leaveRequestRepo.GetAllLeaveRequest(personalId);
            if(allLeaveRequests.Any())
            {
                response.Result = _mapper.Map<IEnumerable<LeaveRequestReadDTO>>(allLeaveRequests);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("get/{personalId}/{leaveRequestId}")]
        public async Task<IActionResult> GetLeaveRequestById(int leaveRequestId, int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var leaveRequestSingle = await _leaveRequestRepo.GetLeaveRequestById(leaveRequestId, personalId);
            if(leaveRequestSingle != null)
            {
                response.Result = _mapper.Map<LeaveRequestReadDTO>(leaveRequestSingle);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestCreateDTO leaveRequestCreateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if(leaveRequestCreateDTO != null)
            {
                await _leaveRequestRepo.CreateLeaveRequest(_mapper.Map<LeaveRequest>(leaveRequestCreateDTO));
                response.Result = leaveRequestCreateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;

                return Created("Created", response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete/{personalId}/{leaveRequestId}")]
        public async Task<IActionResult> DeleteLeaveRequest(int leaveRequestId, int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var isDeleted = await _leaveRequestRepo.DeleteLeaveRequest(leaveRequestId, personalId);
            if (isDeleted)
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}