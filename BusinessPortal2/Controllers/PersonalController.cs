using AutoMapper;
using Azure;
using Azure.Core;
using BusinessPortal2.Models;
using BusinessPortal2.Models.DTO.PersonalDTO;
using BusinessPortal2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace BusinessPortal2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : Controller
    {
        private readonly IPersonalRepo repo;
        private readonly ILeaveTypeRepository leaveRepo;
        public PersonalController(IPersonalRepo _repo, ILeaveTypeRepository _leaveRepo)
        {
            this.repo = _repo;
            this.leaveRepo = _leaveRepo;
        }

        [HttpGet("GetAllPersonals")]
        public async Task<IActionResult> GetAllPersonals()
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

            var allPersonals = await repo.GetAll();
            if (allPersonals.Any())
            {
                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.body = allPersonals;

                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Register")]
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
            var personal = await repo.Register(_mapper.Map<Personal>(r_Personal_DTO));

            LeaveType leaveRype = new LeaveType()
            {
                PersonalId = personal.Id, 
                Sick = 25, 
                Vabb = 25, 
                Vacation = 25
            };

            await leaveRepo.CreateLeave(leaveRype);

            response.isSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.Created;

            return Created("Created", response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePersonal([FromBody] PersonalUpdateDTO p_Update_DTO, [FromServices] IMapper _mapper,
            [FromServices] IValidator<PersonalUpdateDTO> _validate)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var pToUpdate = await repo.GetPersonalById(p_Update_DTO.Id);

            if (pToUpdate == null)
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

            pToUpdate.FullName = p_Update_DTO.FullName;
            pToUpdate.Email = p_Update_DTO.Email;
            pToUpdate.isAdmin = p_Update_DTO.isAdmin;

            await repo.Update(pToUpdate);

            response.isSuccess = true;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(response);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> PersonalDelete(int id)
        {
            ApiResponse response = new ApiResponse() { isSuccess = false, StatusCode = System.Net.HttpStatusCode.NotFound };

            var personToDelete = await repo.GetPersonalById(id);
            if(personToDelete != null)
            {
                await leaveRepo.DeleteLeave(personToDelete.Id);
                await repo.Delete(personToDelete);

                response.isSuccess = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;

                return Ok(response);
            }

            return NotFound();
        }
    }
}
