namespace JwtAuthDemo.Contracts.DA
{
    public interface IJwtTokenRepo
    {
        string GetRefreshToken(string username);
        void DeleteRefreshToken(string username, string refreshToken);
        void SaveRefreshToken(string username, string newRefreshToken);
    }
}
