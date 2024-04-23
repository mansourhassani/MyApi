using Data.Contracts;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using Services.Services;
using WebFramework.Api;

namespace MyApi.Controllers.v2
{
    [ApiVersion("2")]
    public class UsersController : v1.UsersController
    {
        public UsersController(IUserRepository userRepository,
            ILogger<v1.UsersController> logger,
            IJwtService jwtService,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager) 
            : base(userRepository, logger, jwtService, userManager, roleManager, signInManager)
        {
        }

        public override Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            return base.Create(userDto, cancellationToken);
        }

        public override Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }

        public override Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }

        public override Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            return base.Get(id, cancellationToken);
        }

        public override Task<string> Token(string username, string password, CancellationToken cancellationToken)
        {
            return base.Token(username, password, cancellationToken);
        }

        public override Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            return base.Update(id, user, cancellationToken);
        }
    }
}
