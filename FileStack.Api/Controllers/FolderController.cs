using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController(IMediator mediator) : ControllerBase
    {

        //Folder Endpoints

        //1 . Create Folder
        //2 . Get Folder by Id
        //3 . Get All Folders for User
        //4 . Update Folder (naming)
        //5 . Delete Folder
        //6 . Move Folder to another Folder
        //7 . Get Subfolders of a Folder


    }
}
