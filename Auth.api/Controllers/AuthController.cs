using Microsoft.AspNetCore.Mvc;
using Auth.common.Model;  
using Auth.common.TokenJwt;  
using Microsoft.AspNetCore.Identity;
using Auth.common.Dto;
using Auth.common.Repository;
//using System.Diagnostics;

namespace Auth.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthRepository _AuthRepo) : ControllerBase
    {
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterUser signupDto)
        {

            if (signupDto == null || string.IsNullOrWhiteSpace(signupDto.username))
            {
                return BadRequest("Invalid request payload.");
            }

            var result = await _AuthRepo.SignUp(signupDto);

            return Ok(result);


        }




        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.username) || string.IsNullOrWhiteSpace(login.password))
            {
                return BadRequest("Invalid login request payload.");
            }

            var response = await _AuthRepo.Login(login);

            if (response == "User not found." || response == "Invalid credentials.")
            {
                return Unauthorized(response); 
            }

            if (!string.IsNullOrWhiteSpace(response))
            {
                return Ok(new { Token = response });
            }

            return StatusCode(500, "An unexpected error occurred during login.");
        }

    }
}
