using AutoMapper;
using Azure;
using Azure.Core;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using BusinessPortal2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

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
            var result = _validate.Validate(r_Personal_DTO);

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
                var test = CreateToken(loginResult.User);
                return Ok(test);
            }

            return BadRequest("No token");
        }

        private string CreateToken(Personal User)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("jti", Guid.NewGuid().ToString()),
                new Claim("id", User.Id.ToString()),
                new Claim("email", User.Email),
                User.isAdmin ? new Claim("role", "admin") : new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                confirguration.GetSection("Appsettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
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
