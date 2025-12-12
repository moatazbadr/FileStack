using FileStack.Application.APIResponses;
using FileStack.Application.DTOS;
using FileStack.Application.Interfaces;
using FileStack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FileStack.Application.User.Command.UserLoginCommand
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, LoginResponse>
    {
        private readonly IauthService iauthService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserLoginCommandHandler(IauthService iauthService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.iauthService = iauthService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<LoginResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var userlogin = new LoginRequestDTO()
            {
                Email = request.Email,
                Password = request.Password,
            };
             
            var response = await iauthService.LoginAsync(userlogin);  
            return response;
            
        }
    }
}
