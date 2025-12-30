using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FileStack.Application.User;

public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext  
{
    public CurrentUser GetCurrentUser()
    {
        var httpContext = _httpContextAccessor.HttpContext;
       if (httpContext == null || !httpContext.User.Identity!.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }
        var UserId = httpContext.User.FindFirst(Ui => Ui.Type == ClaimTypes.NameIdentifier)!.Value;
        var email =httpContext.User.FindFirst(ui=>ui.Type == ClaimTypes.Email)!.Value;
        var roles =httpContext.User.FindAll(ui=>ui.Type==ClaimTypes.Role).Select(r=>r.Value).ToList();
        var DateOBirth =httpContext.User.FindFirst(ui=>ui.Type== "BirthDate")!.Value;
        var FirstName = httpContext.User.FindFirst(ui => ui.Type == "FirstName")!.Value;
        var LastName = httpContext.User.FindFirst(ui => ui.Type == "LastName")!.Value;
        var FormattedDateOfBirth = DateOBirth == null ? (DateOnly?)null : DateOnly.ParseExact(DateOBirth,"dd-MM-yy",null);
        return new CurrentUser(UserId,FirstName,LastName,FormattedDateOfBirth,email,roles);



    }
}
