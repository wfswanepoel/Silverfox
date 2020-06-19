namespace Application.Infrastructure.HttpClients.Interfaces
{
    public interface IAuthenticateResponse
    {
        string AccessToken { get; set; }
        string TokenType { get; set; }
        int ExpiresIn { get; set; }
    }
}
