using System.Net.Mime;
using ChambaPro.Platform.API.IAM.Domain.Services;
using ChambaPro.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Authorization;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.IAM.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
namespace ChambaPro.Platform.API.IAM.Interfaces.Rest;


[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    
    private readonly IStringLocalizer<AuthenticationController> _localizer;

    public AuthenticationController(IUserCommandService userCommandService, IStringLocalizer<AuthenticationController> localizer)
    {
        _userCommandService = userCommandService;
        _localizer = localizer;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        try
        {
            var result = await _userCommandService.Handle(signInCommand);
            var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler
                .ToResourceFromEntity(result.Item1!, result.Item2);

            return Ok(authenticatedUserResource);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler
            .ToCommandFromResource(resource);

        try
        {
            var result = await _userCommandService.Handle(signUpCommand);
                
            if (result is null)
                return BadRequest(new { message = _localizer["UserRegistrationFailed"] });
            return Ok(new { message = _localizer["UserRegisteredSuccessfully"] });        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}