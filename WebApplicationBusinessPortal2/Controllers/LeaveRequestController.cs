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
using WebApplicationBusinessPortal2.Services;

namespace WebApplicationBusinessPortal2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaveRequestController : Controller
    {
        private readonly IHttpClientService httpClientService;
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(IHttpClientService httpClientService, ILeaveRequestService leaveRequestService)
        {
            this.httpClientService = httpClientService;
            _leaveRequestService = leaveRequestService;

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
        public ActionResult Details(int leaveRequestId)
        {
            LeaveRequestReadDTO leaveRequest = new LeaveRequestReadDTO();
            HttpResponseMessage responseMessage = httpClientService.Client
                .GetAsync(httpClientService.Client.BaseAddress + "user/leaverequest/get/" + leaveRequestId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                leaveRequest = JsonConvert.DeserializeObject<LeaveRequestReadDTO>(data);
            }
            return View(leaveRequest);
        }

        // GET: LeaveRequestController/Create
        public ActionResult Create()
        {
            var leaveTypeList = GetSelectListFromApi<LeaveTypeSimpleReadDTO>("leavetype/getall");
            var leaveTypeSelectList = leaveTypeList.Select(leaveType => new SelectListItem
            {
                Text = leaveType.LeaveName,
                Value = leaveType.Id.ToString()
            }).ToList();

            ViewBag.LeaveType = leaveTypeSelectList;

            return View();
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveRequestCreateDTO leaveRequestToCreate)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(leaveRequestToCreate);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = httpClientService.Client
                    .PostAsync(httpClientService.Client.BaseAddress + "user/leaverequest/create", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(leaveRequestToCreate);
        }

        // POST: LeaveRequestController/Delete/5
        [HttpDelete("{leaveRequestId}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int leaveRequestId, int personalId)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage = httpClientService.Client
                    .DeleteAsync(httpClientService.Client.BaseAddress + "user/leaverequest/delete/" + leaveRequestId).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private List<T> GetSelectListFromApi<T>(string apiEndpoint)
        {
            var availableData = new List<T>();
            HttpResponseMessage response = httpClientService.Client.GetAsync(httpClientService.Client.BaseAddress + apiEndpoint).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                availableData = JsonConvert.DeserializeObject<List<T>>(data);
            }

            return availableData;
        }
    }
}
