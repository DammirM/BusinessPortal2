using AutoMapper;
using Azure;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO;
using BusinessPortal2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal2.Controllers
{
    [ApiController]
    public class PersonalController : Controller
    {
        private readonly IPersonalRepo repo;
        public PersonalController(IPersonalRepo _repo)
        {
            this.repo = _repo;
        }

        public async Task<IActionResult> Register([FromBody] RegisterPersonalDTO r_Personal_DTO, [FromServices] IMapper _mapper,
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
            await repo.Register(_mapper.Map<Personal>(r_Personal_DTO));

            response.isSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;

            return Created("Created", response);
        }
    }
}
