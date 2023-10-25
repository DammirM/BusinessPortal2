using Azure;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApplicationBusinessPortal2.Models;
using WebApplicationBusinessPortal2.Models.ViewModels;
using WebApplicationBusinessPortal2.Services;

namespace WebApplicationBusinessPortal2.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IGetSelectListService _getSelectListService;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, IGetSelectListService getSelectListService)
        {
            _leaveRequestService = leaveRequestService;
            _getSelectListService = getSelectListService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int personalId = GetUserIdFromToken();
            List<LeaveRequestReadDTO> leaveRequests = new List<LeaveRequestReadDTO>();
            var response = await _leaveRequestService.GetLeaveRequestAsync<AppResponse>(personalId);
            if (response.IsSuccess)
            {   
                leaveRequests = JsonConvert.DeserializeObject<List<LeaveRequestReadDTO>>(response.Result.ToString());
            }
            return View(leaveRequests);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int leaveRequestId)
        {
            LeaveRequestReadDTO leaveRequest = new LeaveRequestReadDTO();
            var response = await _leaveRequestService.GetLeaveRequestByIdAsync<AppResponse>(1, leaveRequestId);
            if (response.IsSuccess)
            {
                leaveRequest = JsonConvert.DeserializeObject<LeaveRequestReadDTO>(response.Result.ToString());
            }
            return View(leaveRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> leaveTypeSelectList = new List<SelectListItem>();
            List<LeaveTypeSimpleReadDTO> leaveTypes = new List<LeaveTypeSimpleReadDTO>();
            var response = await _getSelectListService.GetSelectListAsync<AppResponse>("leavetypes/getall");
            if (response.IsSuccess)
            {
                leaveTypes = JsonConvert.DeserializeObject<List<LeaveTypeSimpleReadDTO>>(response.Result.ToString());

                foreach (var leaveType in leaveTypes)
                {
                    leaveTypeSelectList.Add(new SelectListItem { Text = leaveType.LeaveName, Value = leaveType.Id.ToString() });
                }
            }
            ViewBag.LeaveTypes = leaveTypeSelectList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create([FromForm] LeaveRequestCreateDTO leaveRequestToCreate)
        {
            if (ModelState.IsValid)
            {
                int PersonalId = GetUserIdFromToken();

                if (PersonalId != 0)
                {
                    leaveRequestToCreate.PersonalId = PersonalId;
                    var response = await _leaveRequestService.CreateLeaveRequestAsync<AppResponse>(leaveRequestToCreate);
                    if (response.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(leaveRequestToCreate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int leaveRequestId)
        {
            int personalId = 1;
            LeaveRequestReadDTO leaveRequest = new LeaveRequestReadDTO();
            var response = await _leaveRequestService.GetLeaveRequestByIdAsync<AppResponse>(personalId, leaveRequestId);
            if (response.IsSuccess)
            {
                leaveRequest = JsonConvert.DeserializeObject<LeaveRequestReadDTO>(response.Result.ToString());
            }
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm]LeaveRequestReadDTO leaveRequestRead)
        {
            if (ModelState.IsValid)
            {
                int personalId = 1;
                var response = await _leaveRequestService.DeleteLeaveRequestAsync<AppResponse>(personalId, leaveRequestRead.Id);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public int GetUserIdFromToken()
        {
            string tokenFromCookie = Request.Cookies["AuthToken"];

            if (tokenFromCookie != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(tokenFromCookie);
                var rolesClaim = token.Claims.FirstOrDefault(claim => claim.Type == "id");

                if (rolesClaim != null)
                {
                    return int.Parse(rolesClaim.Value);
                }
            }
            return 0;
        }

    }
}
