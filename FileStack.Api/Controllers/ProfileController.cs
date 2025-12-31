using FileStack.Api.Constants;
using FileStack.Application.APIResponses;
using FileStack.Application.User.Command.DeleteProfilePicture;
using FileStack.Application.User.Command.UserProfilePicturePostCommand;
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
    [Authorize(Roles = ValidUserRoles.User)]
    public async Task<IActionResult> GetUserProfile(CancellationToken cancellation)
    {
        var userProfile = await mediator.Send(new GetUserProfileQuery(), cancellation);
        return Ok(userProfile);
    }
    [HttpPost]
    [Route("Profile-Picture")]
    [Authorize(Roles = ValidUserRoles.User)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProfilePicture([FromForm] UserProfilePicturePostCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpPost]
    [Route("Delete-Profile-Picture")]
    public async Task<IActionResult> DeleteProfilePicture()
    {
        var result = await mediator.Send(new DeleteProfilePictureCommand());
        return Ok(result);
    }

}
