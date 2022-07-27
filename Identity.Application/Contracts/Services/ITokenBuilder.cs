namespace Identity.Application.Contracts.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(string username,int userId);
    }
}
