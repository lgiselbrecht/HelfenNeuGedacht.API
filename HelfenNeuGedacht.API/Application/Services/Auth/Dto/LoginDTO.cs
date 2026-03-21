using System.ComponentModel.DataAnnotations;

namespace HelfenNeuGedacht.API.Application.Services.Auth.Dto;

public record LoginDTO
{
    [Required(ErrorMessage = "User Name is required")]
    public required string Username { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; init; }
}
