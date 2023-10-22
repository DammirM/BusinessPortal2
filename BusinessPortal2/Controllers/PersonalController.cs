using AutoMapper;
using Azure;
using Azure.Core;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using BusinessPortal2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace BusinessPortal2.Controllers
{
    [Route("api/personal")]
    [ApiController]
    public class PersonalController : Controller
    {
        private readonly IPersonalRepo repo;
        private readonly IConfiguration confirguration;
        public PersonalController(IPersonalRepo _repo, IConfiguration _configuration)
        {
            this.repo = _repo;
            this.confirguration = _configuration;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPersonals()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allPersonals = await repo.GetAllPersonal();
            if (allPersonals.Any())
            {
                response.body = allPersonals;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            response.Errors.Add("No personals found.");
            return NotFound(response);
        }

        [HttpGet("get/{personalId}")]
        public async Task<IActionResult> GetPersonalById(int personalId)
        {
            ApiResponse response= new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var personalById = await repo.GetPersonalById(personalId);
            if(personalById != null)
            {
                response.body = personalById;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(response);
            }

            response.Errors.Add($"Personal with id=[{personalId}] could not be found");
            return NotFound(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> PersonalRegister([FromBody] RegisterPersonalDTO r_Personal_DTO, [FromServices] IMapper _mapper,
            [FromServices] IValidator<RegisterPersonalDTO> _validate)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };
            var result = await _validate.ValidateAsync(r_Personal_DTO);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.Errors.Add(item.ToString());
                }

                return BadRequest(response);
            }

            r_Personal_DTO.Password = BCrypt.Net.BCrypt.HashPassword(r_Personal_DTO.Password);
            await repo.RegisterPersonal(_mapper.Map<Personal>(r_Personal_DTO));

            response.body = r_Personal_DTO;
            response.isSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;

            return Created("Created", response);
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginPersonal([FromBody] LoginPersonalDTO l_Personal_DTO,
            [FromServices] IMapper _mapper)
        {
            var loginResult = await repo.Login(l_Personal_DTO);
            if (loginResult.IsUserValid)
            {
                var token = CreateToken(loginResult.User);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(1),
                    HttpOnly = true,
                    Secure = true
                };

                Response.Cookies.Append("AuthToken", token, cookieOptions);

                return Ok(token);
            }

            return BadRequest("Login Failed: The provided credentials are invalid or the account does not exist. Please double-check your username and password, and ensure you have registered.");
        }

        private string CreateToken(Personal user)
        {
            var secretKey = confirguration.GetSection("Appsettings:Token").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.FullName), 
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            // Additional claims
            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("email", user.Email));
            if (user.isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "user"));
            }

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",    // Change this to your issuer
                audience: "YourAudience",  // Change this to your audience
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdatePersonal([FromBody] PersonalUpdateDTO p_Update_DTO, [FromServices] IMapper _mapper,
            [FromServices] IValidator<PersonalUpdateDTO> _validate)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var personalToUpdate = await repo.GetPersonalById(p_Update_DTO.Id);

            if (personalToUpdate == null)
            {
                response.Errors.Add("ID not found.");
                return NotFound(response);
            }
            var validationResult = await _validate.ValidateAsync(p_Update_DTO);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(error.ErrorMessage);
                }
                return BadRequest(response);
            }

            await repo.UpdatePersonal(_mapper.Map<Personal>(personalToUpdate));

            response.body = p_Update_DTO;
            response.isSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(response);
        }

        [HttpDelete("delete/{personalId}")]
        public async Task<IActionResult> PersonalDelete(int personalId)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var personToDelete = await repo.GetPersonalById(personalId);
            if(personToDelete != null)
            {
                await repo.DeletePersonal(personToDelete);

                response.body = personToDelete;
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }

            response.Errors.Add($"Personal with id=[{personalId}] could not be found");
            return NotFound(response);
        }
    }
}
