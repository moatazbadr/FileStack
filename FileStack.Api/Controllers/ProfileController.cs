using FileStack.Api.Constants;
using FileStack.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Route("profile-Details")]
    [Authorize(Roles =ValidUserRoles.User)]
    public async Task<IActionResult> GetUserProfile(CancellationToken cancellation)
    {
        var userProfile = await mediator.Send(new GetUserProfileQuery() ,cancellation);
        return Ok(userProfile);
    }

}
