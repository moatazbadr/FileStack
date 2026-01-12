using FileStack.Api.Constants;
using FileStack.Application.Folders.Commands.CreateFolder;
using FileStack.Application.Folders.Commands.RenameFolder;
using FileStack.Application.Folders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStack.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FolderController(IMediator mediator) : ControllerBase
{

    //Folder Endpoints

    //1 . Create Folder
    [HttpPost("create")]
    [Authorize(Roles=ValidUserRoles.User)]
    public async Task<IActionResult> CreateFolder([FromBody] CreateFolderCommand command)
    {
        var result = await mediator.Send(command);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    //2 . Get Folder by Id
   
    
    
    //4 . Update Folder (naming)
    [HttpPatch("rename")]
    [Authorize(Roles=ValidUserRoles.User)]
    public async Task<IActionResult> RenameFolder([FromBody] RenameFolderCommand command)
    {
        var result = await mediator.Send(command);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("folder/{name}")]
    [Authorize(Roles=ValidUserRoles.User)]
    public async Task<IActionResult> GetFolderByName(string name)
    {
        var GetFolderQuery = new GetFolderQuery
        {
            FolderName = name
        };
        var result = await mediator.Send(GetFolderQuery);
        if (result.Count() != 0)
        {
            return Ok(result);
        }
        return NotFound("Folders not found.");

    }

    //3 . Get All Folders for User
    //5 . Delete Folder
    //6 . Move Folder to another Folder
    //7 . Get Subfolders of a Folder


}
