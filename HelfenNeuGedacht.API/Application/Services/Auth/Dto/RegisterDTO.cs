using System.ComponentModel.DataAnnotations;

namespace HelfenNeuGedacht.API.Application.Services.Auth.Dto;

public record RegisterDTO
{
    [Required(ErrorMessage = "User Name is required")]
    public required string Username { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public required string Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; init; }
}
