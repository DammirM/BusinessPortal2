using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.LeaveRequestDTO;
using BusinessPortal2.Models.DTO.LeaveTypeDTO;
using BusinessPortal2.Models.DTO.PersonalDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> PersonalIndex()
        {

            List<PersonalReadDTO> list = new List<PersonalReadDTO>();

            var response = await _leaveRequestAdminService.GetPersonalAdminAsync<AppResponse>();

            if (response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PersonalReadDTO>>(response.Result.ToString());
            }
            return View(list);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> LeaveTypeIndex()
        {
            List<LeaveTypeReadDTO> list = new List<LeaveTypeReadDTO>();

            var response = await _leaveRequestAdminService.GetLeaveTypeAsync<AppResponse>();

            if (response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<LeaveTypeReadDTO>>(response.Result.ToString());

                list = list.GroupBy(lt => lt.LeaveName)
                           .Select(group => group.First())
                           .ToList();
            }

            return View(list);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Approved()
        {

            List<LeaveRequestReadAdminDTO> approvedRequests = new List<LeaveRequestReadAdminDTO>();

            var response = await _leaveRequestAdminService.GetLeaveRequestAdminAsync<AppResponse>();

            if (response.IsSuccess)
            {
                List<LeaveRequestReadAdminDTO> allRequests = JsonConvert.DeserializeObject<List<LeaveRequestReadAdminDTO>>(response.Result.ToString());

                approvedRequests = allRequests.Where(request => request.ApprovalState == "Approved").ToList();
            }
            return View(approvedRequests);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Rejected()
        {

            List<LeaveRequestReadAdminDTO> approvedRequests = new List<LeaveRequestReadAdminDTO>();

            var response = await _leaveRequestAdminService.GetLeaveRequestAdminAsync<AppResponse>();

            if (response.IsSuccess)
            {
                List<LeaveRequestReadAdminDTO> allRequests = JsonConvert.DeserializeObject<List<LeaveRequestReadAdminDTO>>(response.Result.ToString());

                approvedRequests = allRequests.Where(request => request.ApprovalState == "Rejected").ToList();
            }
            return View(approvedRequests);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Pending()
        {

            List<LeaveRequestReadAdminDTO> approvedRequests = new List<LeaveRequestReadAdminDTO>();

            var response = await _leaveRequestAdminService.GetLeaveRequestAdminAsync<AppResponse>();

            if (response.IsSuccess)
            {
                List<LeaveRequestReadAdminDTO> allRequests = JsonConvert.DeserializeObject<List<LeaveRequestReadAdminDTO>>(response.Result.ToString());

                approvedRequests = allRequests.Where(request => request.ApprovalState == "Pending").ToList();
            }
            return View(approvedRequests);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> leaveTypeSelectList = new List<SelectListItem>();
            List<LeaveTypeSimpleReadDTO> leaveTypes = new List<LeaveTypeSimpleReadDTO>();
            var response = await _getSelectListService.GetSelectListAsync<AppResponse>("leavetypes/getall");

            if (response.IsSuccess)
            {
                leaveTypes = JsonConvert.DeserializeObject<List<LeaveTypeSimpleReadDTO>>(response.Result.ToString());

                leaveTypes = leaveTypes.GroupBy(x => x.LeaveName).Select(group => group.First()).ToList();

                foreach (var leaveType in leaveTypes)
                {
                    leaveTypeSelectList.Add(new SelectListItem { Text = leaveType.LeaveName, Value = leaveType.Id.ToString() });
                }
            }
            ViewBag.LeaveTypes = leaveTypeSelectList;

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LeaveRequestCreateDTO Dto)
        {
            Dto.PersonalId = GetUserIdFromToken();

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

        [Authorize(Roles = "admin")]
        public async Task <IActionResult> Delete(int Id)
        {
            var response = await _leaveRequestAdminService.DeleteLeaveRequestAdminAsync<AppResponse>(Id);
            
                return RedirectToAction(nameof(AdminIndex));
            
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteLeave(string name)
        {

            var response = await _leaveRequestAdminService.DeleteLeaveTypeByNameAsync<AppResponse>(name);


            return RedirectToAction(nameof(LeaveTypeIndex));

        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateLeave(int Id)
        {
            var response = await _leaveRequestAdminService.GetLeaveTypeByIdtAdminAsync<AppResponse>(Id);

            if (response != null && response.IsSuccess)
            {
                LeaveTypeUpdateDTO model = JsonConvert.DeserializeObject<LeaveTypeUpdateDTO>(Convert.ToString(response.Result));

                return View(model);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateLeave(LeaveTypeUpdateDTO leaveDTO)
        {
            if (ModelState.IsValid)
            {
                var updateResponse = await _leaveRequestAdminService.UpdateLeaveTypeAdminAsync<AppResponse>(leaveDTO);

                if (updateResponse != null && updateResponse.IsSuccess)
                {
                    return RedirectToAction("InfoPersonal", new { Id = leaveDTO.PersonalId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update leave information. Please try again.");
                }
            }

            // ModelState is not valid or update failed, redisplay the form with validation errors.
            return View(leaveDTO);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateLeave()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLeave(LeaveTypeCreateDTO leaveDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestAdminService.CreateLeveType<AppResponse>(leaveDTO);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(LeaveTypeIndex));
                }
            }
            return View(leaveDTO);
        }
        


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeletePersonal(int Id)
        {

            var response = await _leaveRequestAdminService.DeletePersonalAsync<AppResponse>(Id);

            return RedirectToAction(nameof(PersonalIndex));

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> UpdatePersonal(int Id)
        {

            var response = await _leaveRequestAdminService.GetPersonalByIdAdminAsync<AppResponse>(Id);

            if (response != null && response.IsSuccess)
            {
                PersonalUpdateDTO model = JsonConvert.DeserializeObject<PersonalUpdateDTO>(Convert.ToString(response.Result));

                return View(model);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdatePersonal(PersonalUpdateDTO PersonalDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _leaveRequestAdminService.UpdatePersonalAdminAsync<AppResponse>(PersonalDTO);

                if (response != null && response.IsSuccess)
                {

                    return RedirectToAction(nameof(PersonalIndex));
                }
            }
            return View(PersonalDTO);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> InfoPersonal(int Id)
        {

            var response = await _leaveRequestAdminService.GetLeaveTypeByPersonalId<AppResponse>(Id);

            if (response != null && response.IsSuccess)
            {
                List<LeaveTypeReadDTO> model = JsonConvert.DeserializeObject<List<LeaveTypeReadDTO>>(Convert.ToString(response.Result));

                return View(model);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
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
