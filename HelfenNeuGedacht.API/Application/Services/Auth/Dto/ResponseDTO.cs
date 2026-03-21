namespace HelfenNeuGedacht.API.Application.Services.Auth.Dto;

public record ResponseDTO
{
    public bool Success { get; init; }
    public required string Status { get; init; }
    public required string Message { get; init; }
}
