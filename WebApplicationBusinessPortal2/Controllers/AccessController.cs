using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Common;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                else if(!response.IsSuccess && response.Errors.Count > 0)
                {
                    foreach (var errorMessage in response.Errors)
                    {
                        ModelState.Clear();
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

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(response.Result.ToString());

                var rolesClaim = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

                if (rolesClaim != null)
                {
                    string role = rolesClaim.Value;

                    if (role == "user")
                    {
                        return RedirectToAction("Index", "LeaveRequest");
                    }
                    else if (role == "admin")
                    {
                        return RedirectToAction("AdminIndex", "LeaveRequestAdmin");
                    }
                }
            }
            else if (!response.IsSuccess && response.Errors.Count > 0)
            {
                foreach (var errorMessage in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction(nameof(Login));
        }

        public void CreateCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1),
                HttpOnly = false,
            };

            Response.Cookies.Append("AuthToken", token, cookieOptions);
        }
    }
}
