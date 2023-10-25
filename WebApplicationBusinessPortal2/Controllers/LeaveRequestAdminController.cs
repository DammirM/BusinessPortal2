using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
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
        private readonly IGetSelectListService _getSelectListService;

        public LeaveRequestAdminController(IGetSelectListService getSelectListService, IHttpClientService httpClientService, ILeaveRequestAdminService leaveRequestAdminService)
        {
            this.httpClientService = httpClientService;
            _leaveRequestAdminService = leaveRequestAdminService;
            _getSelectListService= getSelectListService;

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

        public async Task<IActionResult> Create([FromForm] LeaveRequestCreateDTO Dto)
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
