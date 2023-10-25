using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplicationBusinessPortal2.Models;
using WebApplicationBusinessPortal2.Services;

namespace WebApplicationBusinessPortal2.Controllers
{
    
    public class LeaveRequestAdminController : Controller
    {

        private readonly IHttpClientService httpClientService;
        private readonly ILeaveRequestAdminService _leaveRequestAdminService;
        private readonly ILogger<LeaveRequestAdminController> _logger;

        public LeaveRequestAdminController(ILogger<LeaveRequestAdminController> logger, IHttpClientService httpClientService, ILeaveRequestAdminService leaveRequestAdminService)
        {
            this.httpClientService = httpClientService;
            _leaveRequestAdminService = leaveRequestAdminService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> AdminIndex()
        {

            List<LeaveRequestReadAdminDTO> list = new List<LeaveRequestReadAdminDTO>();

            var response = await _leaveRequestAdminService.GetLeaveRequestAdminAsync<AppResponse>();

            if (response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LeaveRequestReadAdminDTO>>(response.Result.ToString());
            }
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(LeaveRequestCreateDTO Dto)
        {
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestAdminService.CreateLeaveRequestAdminAsync<AppResponse>(Dto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(AdminIndex));
                }
            }

            return View(Dto);
        }

        public async Task <IActionResult> Delete(int Id)
        {
            var response = await _leaveRequestAdminService.DeleteLeaveRequestAdminAsync<AppResponse>(Id);
            
                return RedirectToAction(nameof(AdminIndex));
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var response = await _leaveRequestAdminService.GetLeaveRequestByIdAdminAsync<AppResponse>(Id);

            if (response != null && response.IsSuccess)
            {
                LeaveRequestUpdateDTO model = JsonConvert.DeserializeObject<LeaveRequestUpdateDTO>(Convert.ToString(response.Result));

                // Populate the ViewBag with ApprovalState options
                ViewBag.ApprovalStateOptions = new SelectList(new[]
                {
            new SelectListItem { Text = "Pending", Value = "Pending" },
            new SelectListItem { Text = "Rejected", Value = "Rejected" },
            new SelectListItem { Text = "Approved", Value = "Approved" }
        }, "Value", "Text");

                return View(model);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Update(LeaveRequestUpdateDTO leaveRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestAdminService.UpdateLeaveRequesAdminAsync<AppResponse>(leaveRequest);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(AdminIndex));
                }
            }

            return View(leaveRequest);
        }



    }
}
