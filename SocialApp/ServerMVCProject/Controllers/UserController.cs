namespace ServerMVCProject.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;
    using ServerMVCProject.Models;

    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return this.View(new AuthenticationModel());
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return this.View(new AuthenticationModel());
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] AuthenticationModel user)
        {
            try
            {
                if (user.Username != null && user.Password != null)
                {
                    long result = this._userService.AddUser(user.Username, " ", user.Password, "");

                    this.HttpContext.Session.SetString("UserId", result.ToString());

                    // at this point, the register is successful
                    // here you redirect to Body Metrics page (for registering new user)
                    return this.RedirectToAction("Index", "ViewPosts");
                }
                else
                {
                    return this.View("Error", new ErrorViewModel { ErrorMessage = "Username and Password cannot be null" });
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
                    ErrorMessage = ex.Message,
                };
                return this.View("Error", errorModel);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] AuthenticationModel user)
        {
            try
            {
                if (user.Username != null && user.Password != null)
                {
                    long result = this._userService.Login(user.Username, user.Password);
                    if (result == -2)
                    {
                        throw new Exception("User doesn't exist");
                    }
                    else if (result == -1)
                    {
                        throw new Exception("Password is incorrect");
                    }

                    this.HttpContext.Session.SetString("UserId", result.ToString());

                    // at this point, the register is successful
                    // here you redirect to Main page (Dashboard)
                    return this.RedirectToAction("Index", "ViewPosts");
                }
                else
                {
                    return this.View("Error", new ErrorViewModel { ErrorMessage = "Username and Password cannot be null" });
                }
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
                    ErrorMessage = ex.Message,
                };

                return this.View("Error", errorModel);
            }
        }
    }
}