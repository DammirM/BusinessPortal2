using Azure;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveRequestDTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using WebApplicationBusinessPortal2.Models;
using WebApplicationBusinessPortal2.Models.ViewModels;
using WebApplicationBusinessPortal2.Services;

namespace WebApplicationBusinessPortal2.Controllers
{
    [Route("LeaveRequest")]
    [ApiController]
    public class LeaveRequestController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly IGetSelectListService _getSelectListService;

        public LeaveRequestController(IHttpClientService httpClientService, ILeaveRequestService leaveRequestService, IGetSelectListService getSelectListService)
        {
            _httpClientService = httpClientService;
            _leaveRequestService = leaveRequestService;
            _getSelectListService = getSelectListService;
        }

        // GET: LeaveRequestController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int personalId = 1;
            List<LeaveRequestReadDTO> leaveRequests = new List<LeaveRequestReadDTO>();
            var response = await _leaveRequestService.GetLeaveRequestAsync<AppResponse>(personalId);
            if (response.IsSuccess)
            {   
                leaveRequests = JsonConvert.DeserializeObject<List<LeaveRequestReadDTO>>(response.Result.ToString());
            }
            return View(leaveRequests);
        }

        // GET: LeaveRequestController/Details/5
        [HttpGet("{leaveRequestId}", Name = "Details")]
        public async Task<IActionResult> Details(int leaveRequestId)
        {
            LeaveRequestReadDTO leaveRequest = new LeaveRequestReadDTO();
            HttpResponseMessage responseMessage = _httpClientService.Client
                .GetAsync(_httpClientService.Client.BaseAddress + "user/leaverequest/get/" + leaveRequestId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                leaveRequest = JsonConvert.DeserializeObject<LeaveRequestReadDTO>(data);
            }
            return View(leaveRequest);
        }

        // GET: LeaveRequestController/Create
        [HttpGet("Create", Name = "Create")]
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

        // POST: LeaveRequestController/Create
        [HttpPost("Create", Name = "Create")]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Create([FromForm] LeaveRequestCreateDTO leaveRequestToCreate)
        {
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestService.CreateLeaveRequestAsync<AppResponse>(leaveRequestToCreate);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(leaveRequestToCreate);
        }

        // POST: LeaveRequestController/Delete/5
        [HttpDelete("{leaveRequestId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int leaveRequestId, int personalId)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage = _httpClientService.Client
                    .DeleteAsync(_httpClientService.Client.BaseAddress + "user/leaverequest/delete/" + leaveRequestId).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
