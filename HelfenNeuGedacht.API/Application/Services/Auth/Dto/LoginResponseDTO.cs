namespace HelfenNeuGedacht.API.Application.Services.Auth.Dto
{
    public class LoginResponseDTO
    {

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
      
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; } 
        

    }
}
