using AutoMapper;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO.LeaveRequestDTO;
using BusinessPortal2.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepo leaveRequestRepo;
        private readonly LeaveRequestAdminRepo _leaveRequestAdminRepo;
        private readonly IMapper _mapper;

        public LeaveRequestController(ILeaveRequestRepo repo, LeaveRequestAdminRepo leaveRequestAdminRepo, IMapper mapper)
        {
            leaveRequestRepo = repo;
            _leaveRequestAdminRepo = leaveRequestAdminRepo;
            _mapper = mapper;
        }

        [HttpGet("getall-leaverequests/{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allLeaveRequests = await leaveRequestRepo.GetAll(id);
            if(allLeaveRequests.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.body = _mapper.Map<IEnumerable<LeaveRequestReadDTO>>(allLeaveRequests);

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("admin/get-leaverequests")]
        public async Task<IActionResult> GetAllAdmin()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allRequests = await _leaveRequestAdminRepo.GetAll();
            if(allRequests.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.body = _mapper.Map<IEnumerable<LeaveRequestReadAdminDTO>>(allRequests);

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("get-leaverequest/{personalId}/{id}")]
        public async Task<IActionResult> GetLeaveRequestById(int id, int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var leaveRequestSingle = await leaveRequestRepo.GetById(id, personalId);
            if(leaveRequestSingle != null)
            {
                response.body = _mapper.Map<LeaveRequestReadDTO>(leaveRequestSingle);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("admin/get-leaverequest/{id}")]
        public async Task<IActionResult> GetLeaveRequestById(int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var leaveRequestSingle = await _leaveRequestAdminRepo.GetById(id);
            if (leaveRequestSingle != null)
            {
                response.body = _mapper.Map<LeaveRequestReadDTO>(leaveRequestSingle);
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("post-leaverequest")]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] LeaveRequestCreateDTO LeaveRequestCreateDTO,
            [FromServices] IMapper _mapper)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if(LeaveRequestCreateDTO != null)
            {
                var newLeaveTypeRequest = await leaveRequestRepo.CreateLeaveRequuest(_mapper.Map<LeaveRequest>(LeaveRequestCreateDTO));
                response.body = newLeaveTypeRequest;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("put-leaverequest")]
        public async Task<IActionResult> UpdateLeaveRequest( 
            [FromBody] LeaveRequestUpdateDTO LeaveRequestUpdateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            if(LeaveRequestUpdateDTO != null)
            {
                await _leaveRequestAdminRepo.Update(_mapper.Map<LeaveRequest>(LeaveRequestUpdateDTO));
                response.body = LeaveRequestUpdateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete-leaverequest")]
        public async Task<IActionResult> DeleteLeaveReqeust([FromServices] LeaveRequestAdminRepo leaveRequestAdminRepo, int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var isDeleted = await leaveRequestAdminRepo.Delete(id);
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