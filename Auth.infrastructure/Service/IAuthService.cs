using Auth.common.Dto;

namespace Auth.infrastructure.Service
{
    internal interface IAuthService
    {
        Task<string> Login(LoginUser login);
        Task<string> SignUp(RegisterUser signup);
    }
}
