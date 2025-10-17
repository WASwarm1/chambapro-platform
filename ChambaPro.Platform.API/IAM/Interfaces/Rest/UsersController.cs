using System.Net.Mime;
using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Commands;
using ChambaPro.Platform.API.IAM.Domain.Model.Queries;
using ChambaPro.Platform.API.IAM.Domain.Services;
using ChambaPro.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Authorization;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Transform;

using Microsoft.AspNetCore.Mvc;

namespace ChambaPro.Platform.API.IAM.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class UsersController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
        private readonly IUserQueryService _userQueryService;

        public UsersController(
            IUserCommandService userCommandService,
            IUserQueryService userQueryService)
        {
            _userCommandService = userCommandService;
            _userQueryService = userQueryService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var getUserByIdQuery = new GetUserByIdQuery(id);
            var result = await _userQueryService.Handle(getUserByIdQuery);

            if (result is null)
                return NotFound(new { message = "User not found" });

            var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
            return Ok(resource);
        }

        [HttpGet("technicians")]
        public async Task<IActionResult> GetAllTechnicians()
        {
            var getAllTechniciansQuery = new GetAllTechniciansQuery();
            var result = await _userQueryService.Handle(getAllTechniciansQuery);

            var resources = result.Select(UserResourceFromEntityAssembler
                .ToResourceFromEntity);

            return Ok(resources);
        }

        [HttpGet("technicians/by-speciality/{speciality}")]
        public async Task<IActionResult> GetTechniciansBySpeciality(string speciality)
        {
            var query = new GetTechniciansBySpecialityQuery(speciality);
            var result = await _userQueryService.Handle(query);

            var resources = result.Select(UserResourceFromEntityAssembler
                .ToResourceFromEntity);

            return Ok(resources);
        }

        [HttpPut("{id:int}/profile")]
        public async Task<IActionResult> UpdateProfile(
            int id, 
            [FromBody] UpdateProfileResource resource)
        {
            var currentUser = (User?)HttpContext.Items["User"];

            if (currentUser == null || currentUser.Id != id)
                return Unauthorized(new { message = "Unauthorized to update this profile" });

            var command = new UpdateProfileCommand(
                id,
                resource.Name,
                resource.Lastname,
                resource.Phone,
                resource.Avatar
            );

            try
            {
                var result = await _userCommandService.Handle(command);
                
                if (result is null)
                    return BadRequest(new { message = "Profile update failed" });

                var userResource = UserResourceFromEntityAssembler
                    .ToResourceFromEntity(result);
                return Ok(userResource);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("{id:int}/technician-profile")]
        public async Task<IActionResult> UpdateTechnicianProfile(
            int id,
            [FromBody] UpdateTechnicianProfileResource resource)
        {
            var currentUser = (User?)HttpContext.Items["User"];

            if (currentUser == null || currentUser.Id != id)
                return Unauthorized(new { message = "Unauthorized to update this profile" });

            var command = new UpdateTechnicianProfileCommand(
                id,
                resource.Speciality,
                resource.Description,
                resource.Experience,
                resource.HourlyRate,
                resource.IsAvailable
            );

            try
            {
                var result = await _userCommandService.Handle(command);
                
                if (result is null)
                    return BadRequest(new { message = "Profile update failed" });

                var userResource = UserResourceFromEntityAssembler
                    .ToResourceFromEntity(result);
                return Ok(userResource);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var currentUser = (User?)HttpContext.Items["User"];

            if (currentUser == null)
                return Unauthorized(new { message = "Not authenticated" });

            var resource = UserResourceFromEntityAssembler
                .ToResourceFromEntity(currentUser);
            return Ok(resource);
        }
}