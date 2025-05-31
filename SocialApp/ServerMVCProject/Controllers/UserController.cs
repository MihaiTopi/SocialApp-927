﻿namespace ServerMVCProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;
    using ServerMVCProject.Models;
    using System.Diagnostics;

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
                    long result = this._userService.AddUser(user.Username, user.Password, "");

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
                    try
                    {

                        User findUser = this._userService.GetUserByUsername(user.Username);
                        if (findUser.Password.Equals(user.Password))
                        {
                            this.HttpContext.Session.SetString("UserId", findUser.Id.ToString());

                        }
                        else
                        {
                            throw new Exception("Password is incorrect");

                        }
                    }
                    catch(Exception e)
                    {
                        throw new Exception("User doesn't exist");

                    }




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