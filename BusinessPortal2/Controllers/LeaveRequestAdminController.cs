using AutoMapper;
using BusinessPortal2.Models.DTO.LeaveRequestDTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessPortal2.Models;

namespace BusinessPortal2.Controllers
{
    // Admin Leave Request Controller
    [Route("api/admin/leaverequest")]
    [ApiController]
    public class LeaveRequestAdminController : ControllerBase
    {
        private readonly LeaveRequestAdminRepo _leaveRequestAdminRepo;
        private readonly IMapper _mapper;

        public LeaveRequestAdminController(LeaveRequestAdminRepo leaveRequestAdminRepo, IMapper mapper)
        {
            _leaveRequestAdminRepo = leaveRequestAdminRepo;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAdmin()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allRequests = await _leaveRequestAdminRepo.GetAll();
            if (allRequests.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.body = _mapper.Map<IEnumerable<LeaveRequestReadAdminDTO>>(allRequests);

                return Ok(response);
            }
            return BadRequest(response);
        }
        
        [HttpGet("getall/{personalId}")]
        public async Task<IActionResult> GetAllAdmin(int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allLeaveRequests = await _leaveRequestAdminRepo.GetAllLeaveRequest(personalId);
            if (allLeaveRequests.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.body = _mapper.Map<IEnumerable<LeaveRequestReadDTO>>(allLeaveRequests);

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("get/{leaveRequestId}")]
        public async Task<IActionResult> GetLeaveRequestById(int leaveRequestId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var leaveRequestSingle = await _leaveRequestAdminRepo.GetById(leaveRequestId);
            if (leaveRequestSingle != null)
            {
                response.body = _mapper.Map<LeaveRequestReadDTO>(leaveRequestSingle);
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

            if (leaveRequestCreateDTO != null)
            {
                await _leaveRequestAdminRepo.CreateLeaveRequest(_mapper.Map<LeaveRequest>(leaveRequestCreateDTO));
                response.body = leaveRequestCreateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateLeaveRequest([FromBody] LeaveRequestUpdateDTO leaveRequestUpdateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if (leaveRequestUpdateDTO != null)
            {
                await _leaveRequestAdminRepo.Update(_mapper.Map<LeaveRequest>(leaveRequestUpdateDTO));
                response.body = leaveRequestUpdateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var isDeleted = await _leaveRequestAdminRepo.Delete(id);
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
