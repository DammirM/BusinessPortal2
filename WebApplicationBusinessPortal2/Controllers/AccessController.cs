using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Common;
using WebApplicationBusinessPortal2.Models;
using WebApplicationBusinessPortal2.Services;

namespace WebApplicationBusinessPortal2.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAccessService _accessService;
        private readonly IHttpClientService _httpClientService;
        public AccessController(IAccessService accessService, IHttpClientService httpClientService)
        {
            _accessService = accessService;
            _httpClientService = httpClientService;

        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterPersonalDTO r_Personal_DTO)
        {
            await Console.Out.WriteLineAsync(r_Personal_DTO.FullName);
            var response = await _accessService.RegisterAsync<AppResponse>(r_Personal_DTO);

            if (response != null)
            {
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Login));
                }

                if (response.ErrorMessages != null && response.ErrorMessages.Count > 0)
                {
                    foreach (var errorMessage in response.ErrorMessages)
                    {
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginPersonalDTO l_Personal_DTO)
        {
            var response = await _accessService.LoginAsync<AppResponse>(l_Personal_DTO);

            if (response != null && response.IsSuccess)
            {
                CreateCookie(response.Result.ToString());
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        public void CreateCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = true,
                Secure = true
            };

            Response.Cookies.Append("AuthToken", token, cookieOptions);
        }
    }
}
