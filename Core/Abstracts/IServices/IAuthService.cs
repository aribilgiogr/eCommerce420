using Core.Concretes.DTOs;
using Tools.Responses;

namespace Core.Abstracts.IServices
{
    public interface IAuthService
    {
        Task<Reply> LoginAsync(LoginDto model);
        Task<Reply> RegisterAsync(RegisterDto model);
        Task LogoutAsync();
    }
}
