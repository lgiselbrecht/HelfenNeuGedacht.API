using HelfenNeuGedacht.API.Application.Services.Auth.Dto;

namespace HelfenNeuGedacht.API.Application.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO model);
        Task<ResponseDTO> RegisterAsync(RegisterDTO model);
    }
}
