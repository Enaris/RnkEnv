using EnvRnk.DataAccess.DbModels;
using EnvRnk.Services.DTOs.Auth;
using EnvRnk.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvRnk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AspUser> userManager;
        private readonly SignInManager<AspUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IRnkUserService rnkUserService;

        public AuthController(UserManager<AspUser> userManager,
            SignInManager<AspUser> signInManager,
            ITokenService tokenService,
            IRnkUserService rnkUserService
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.rnkUserService = rnkUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new AspUser { Email = request.Email, UserName = request.Email };
            var created = await userManager.CreateAsync(user, request.Password);

            if (!created.Succeeded)
            {
                var duplicateEmail = created.Errors.Any(e => e.Code == "DuplicateEmail");
                return BadRequest(duplicateEmail ? "Account with this email address already exists" : "Something wrong have happend");
            }


            var newUser = await userManager.FindByEmailAsync(request.Email);
            await rnkUserService.Create(new Guid(newUser.Id));

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userDb = await userManager.FindByEmailAsync(request.Email);

            if (userDb == null)
                return BadRequest("Login or password is invalid");

            var loginResult = await signInManager.CheckPasswordSignInAsync(userDb, request.Password, false);

            if (!loginResult.Succeeded)
                return BadRequest("Login or password is invalid");

            var token = tokenService.GenerateJwtToken(userDb);

            return Ok(new LoginResponse
            {
                AspUserId = userDb.Id,
                Email = request.Email,
                Token = token
            });
        }
    }
}
