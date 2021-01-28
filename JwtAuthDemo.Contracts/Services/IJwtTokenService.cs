namespace JwtAuthDemo.Contracts.Services
{
    public interface IJwtTokenService
    {
        string GetRefreshToken(string username);
        void DeleteRefreshToken(string username, string refreshToken);
        void SaveRefreshToken(string username, string newRefreshToken);
    }
}
