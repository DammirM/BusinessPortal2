using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO.LeaveRequestDTO;
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

            var allLeaveRequests = await _leaveRequestRepo.GetAll(personalId);
            if(allLeaveRequests.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.body = _mapper.Map<IEnumerable<LeaveRequestReadDTO>>(allLeaveRequests);

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("get/{personalId}/{leaveRequestId}")]
        public async Task<IActionResult> GetLeaveRequestById(int leaveRequestId, int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var leaveRequestSingle = await _leaveRequestRepo.GetById(leaveRequestId, personalId);
            if(leaveRequestSingle != null)
            {
                response.body = _mapper.Map<LeaveRequestReadDTO>(leaveRequestSingle);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestCreateDTO LeaveRequestCreateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if(LeaveRequestCreateDTO != null)
            {
                var newLeaveTypeRequest = await _leaveRequestRepo.Create(_mapper.Map<LeaveRequest>(LeaveRequestCreateDTO));
                response.body = newLeaveTypeRequest;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;

                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}