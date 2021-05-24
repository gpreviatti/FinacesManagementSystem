using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Dtos.User;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class LoginController : BaseController<LoginController>
    {
        private readonly ILoginService _service;
        private readonly IUserService _userService;
        public LoginController(
            ILogger<LoginController> logger, 
            ILoginService service, 
            IUserService userService
        ) : base(logger)
        {
            _service = service;
            _userService = userService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.LoginWeb(login);
                if (result == null)
                {
                    return BadRequest(ModelState);
                }
                await SignInUser(result.User, false);
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        /// <summary>  
        /// Sign In User method.  
        /// </summary>  
        /// <param name="username">Username parameter.</param>  
        /// <param name="isPersistent">Is persistent parameter.</param>  
        /// <returns>Returns - await task</returns>  
        private async Task SignInUser(UserResultDto user, bool isPersistent)
        {
            // Initialization.  
            var claims = new List<Claim>();

            // Setting
            claims.Add(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdenties);
            var authenticationManager = Request.HttpContext;

            // Sign In.  
            await authenticationManager
                .SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent }
                );
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            try
            {
                // Setting.  
                var authenticationManager = Request.HttpContext;

                // Sign Out.  
                authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet("/Login/Register")]
        public IActionResult RegisterIndex() => View("Register");

        [HttpPost("/Login/Register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserCreateDto userCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _userService.CreateAsync(userCreateDto).Result;
                if (result == null)
                {
                    return BadRequest(ModelState);
                }

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                LoggingExceptions(exception);
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
