using AutoMapper;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessPortal2.Models;
using Microsoft.AspNetCore.ResponseCompression;

namespace BusinessPortal2.Controllers
{
    // Admin Leave Request Controller
    [Route("api/admin/leaverequest")]
    [ApiController]
    public class LeaveRequestAdminController : ControllerBase
    {
        private readonly LeaveRequestAdminRepo _leaveRequestAdminRepo;
        private readonly ILeaveTypeRepo _leaveTypeRepo;
        private readonly IMapper _mapper;

        public LeaveRequestAdminController(LeaveRequestAdminRepo leaveRequestAdminRepo, IMapper mapper, ILeaveTypeRepo leaveTypeRepo)
        {
            _leaveRequestAdminRepo = leaveRequestAdminRepo;
            _mapper = mapper;
            _leaveTypeRepo = leaveTypeRepo;
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
                response.Result = _mapper.Map<IEnumerable<LeaveRequestReadAdminDTO>>(allRequests);

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
                response.Result = _mapper.Map<IEnumerable<LeaveRequestReadAdminDTO>>(allLeaveRequests);

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
                response.Result = _mapper.Map<LeaveRequestReadAdminDTO>(leaveRequestSingle);
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

            if (leaveRequestCreateDTO != null && leaveRequestCreateDTO.EndDate > leaveRequestCreateDTO.StartDate)
            {
                await _leaveRequestAdminRepo.CreateLeaveRequest(_mapper.Map<LeaveRequest>(leaveRequestCreateDTO));
                response.Result = leaveRequestCreateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.Created;

                return Created("Created", response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateLeaveRequest([FromBody] LeaveRequestUpdateDTO leaveRequestUpdateDTO)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            TimeSpan daysBetween = leaveRequestUpdateDTO.EndDate - leaveRequestUpdateDTO.StartDate;

            if (leaveRequestUpdateDTO != null)
            {
                await _leaveRequestAdminRepo.Update(_mapper.Map<LeaveRequest>(leaveRequestUpdateDTO));
                response.Result = leaveRequestUpdateDTO;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                if(leaveRequestUpdateDTO.ApprovalState == "Approved")
                {
                    await _leaveTypeRepo.UpdateLeaveTypeOnApproved(daysBetween.Days, leaveRequestUpdateDTO.PersonalId);
                }

                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteLeaveRequest(int Id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var isDeleted = await _leaveRequestAdminRepo.Delete(Id);
            if (isDeleted)
            {
                response.Result = isDeleted;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.NoContent;

                return Ok(response);
            }

            return BadRequest("Not Found");
        }
    }
}
