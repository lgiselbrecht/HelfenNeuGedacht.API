namespace HelfenNeuGedacht.API.Application.Services.Auth.Dto;
public record TokenDTO
{
    public required string token { get; init; }
    public required string id { get; init; }
    public required DateTime expiration { get; init; }
    public required IList<string> roles { get; init; }
}
