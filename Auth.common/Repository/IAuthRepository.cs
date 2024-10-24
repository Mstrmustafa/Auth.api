using Auth.common.Dto;
namespace Auth.common.Repository
{
    public interface IAuthRepository
    {
        Task<string> Login(LoginUser login);

        Task<string> SignUp(RegisterUser signupDto);
    }
}
