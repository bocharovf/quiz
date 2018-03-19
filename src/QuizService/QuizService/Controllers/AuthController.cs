using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizService.Auth;
using QuizService.BusinessLogic;
using QuizService.DataAccess.Auth;
using QuizService.Filters;
using QuizService.Model;
using QuizService.Model.DataContract;
using System.Threading.Tasks;

namespace QuizService.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [QuizExceptionFilter]
    public class AuthController : Controller
    {
        private readonly IAuthenticationWrapperService AuthService;

        public AuthController(IAuthenticationWrapperService authService)
        {
            this.AuthService = authService;
        }

        // POST /auth/register
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationContract registrationContract)
        {
            ThrowIf.Null(registrationContract, nameof(registrationContract));

            var user = new AspnetUser(registrationContract.DisplayName)
            {
                Email = registrationContract.Email
            };

            IdentityResult creationResult = await this.AuthService.CreateAsync(user, registrationContract.Password);
            OperationResultContract response = creationResult.ToOperationResultContract();

            if (!creationResult.Succeeded)
            {
                return BadRequest(response);
            }

            IdentityResult addRoleResult = await this.AuthService.AddToRoleAsync(user, ApplicationRole.User);
            if (!addRoleResult.Succeeded)
            {
                throw new AuthenticationException("Unable to add user to role.", addRoleResult.Errors);
            }

            await this.AuthService.SignInAsync(user, isPersistent: false);

            return Ok(response);
        }

        // POST api/auth/login
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginContract loginContract)
        {
            ThrowIf.Null(loginContract, nameof(loginContract));

            Microsoft.AspNetCore.Identity.SignInResult result;
            result = await this.AuthService.PasswordSignInAsync(
                loginContract.Login, loginContract.Password, loginContract.Remember, 
                lockoutOnFailure: false
            );

            OperationResultContract response = result.ToOperationResultContract();

            if (!result.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // POST api/auth/logout
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.AuthService.SignOutAsync();
            return Ok();
        }

        // GET api/auth/status
        [HttpGet]
        [AllowAnonymous]
        [Route("status")]
        public async Task<IActionResult> Status()
        {
            var contract = new AuthenticationStatusContract()
            {
                IsSignedIn = false
            };

            bool isAuthentificated = this.AuthService.IsAuthenticated(this.User);
            if (isAuthentificated)
            {
                User user = await this.AuthService.GetDomainUserAsync(this.User);
                contract.IsSignedIn = true;
                contract.User = user;
            }

            return Ok(contract);
        }

        
    }
}