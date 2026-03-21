using HelfenNeuGedacht.API.Application.Services.Auth.AuthService;
using HelfenNeuGedacht.API.Application.Services.Auth.Dto;
using HelfenNeuGedacht.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HelfenNeuGedacht.API.Application.Services.AuthService
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        ILogger<AuthService> logger) : IAuthService
    {
        public async Task<LoginResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await userManager.FindByNameAsync(model.Username);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                logger.LogWarning("Failed login attempt for user {Username}", model.Username);

                return new LoginResponseDTO
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            logger.LogInformation("User {Username} logged in successfully", model.Username);

            var token = await tokenService.CreateTokenAsync(user);

            return new LoginResponseDTO
            {
                Success = true,
                Message = "Login successful",
                Token = token.token,
                Expiration = token.expiration
            };
        }

        public async Task<ResponseDTO> RegisterAsync(RegisterDTO model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists is not null)
            {
                return new ResponseDTO
                {
                    Status = "Error",
                    Message = "User already exists!"
                };
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    logger.LogError("User creation error: {Code} - {Description}", error.Code, error.Description);
                }

                return new ResponseDTO
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                };
            }

            await userManager.AddToRoleAsync(user, "User");

            logger.LogInformation("User {Username} registered successfully", model.Username);

            return new ResponseDTO
            {
                Status = "Success",
                Message = "User created successfully!"
            };
        }
    }
}