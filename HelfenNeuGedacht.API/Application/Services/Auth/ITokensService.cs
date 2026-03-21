using HelfenNeuGedacht.API.Application.Services.Auth.Dto;
using HelfenNeuGedacht.API.Domain.Entities;

namespace HelfenNeuGedacht.API.Application.Services.Auth.AuthService
{
    public interface ITokenService
    {
        Task<TokenDTO> CreateTokenAsync(ApplicationUser user);
    }

}
