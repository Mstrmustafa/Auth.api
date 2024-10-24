using Auth.common.Dto;
using Auth.common.Model;
using Auth.common.Repository;
using Auth.common.TokenJwt;
using Microsoft.AspNetCore.Identity;

namespace Auth.infrastructure.Repository
{
    public class AuthRepository(UserManager<AppUser> _userManager,TokenService _tokenService) : IAuthRepository
    {
        async Task<string> IAuthRepository.Login(LoginUser login)
        {

            var user = await _userManager.FindByNameAsync(login.username);
            if (user == null)
            {
                return "User not found.";
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, login.password);
            if (!passwordValid)
            {
                return "Invalid credentials.";
            }

            try
            {
                var token = _tokenService.GenerateToken(user);
                return token;
            }
            catch (Exception ex)
            {
                return $"Error generating token: {ex.Message}";
            }
        }


        async Task<string> IAuthRepository.SignUp(RegisterUser signupDto)
        {
            var userExists = await _userManager.FindByNameAsync(signupDto.username);
            if (userExists != null)
            {
                return "User already exists.";
            }

            var user = new AppUser { UserName = signupDto.username, Email = signupDto.email };
            var result = await _userManager.CreateAsync(user, signupDto.password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return $"Error occurred while creating the user: {errors}";
            }

            return "User created successfully";
        }

    }
}
