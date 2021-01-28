using JwtAuthDemo.Contracts.DA;
using JwtAuthDemo.Contracts.Services;

namespace JwtAuthDemo.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IJwtTokenRepo repo;
        public JwtTokenService(IJwtTokenRepo repo)
        {
            this.repo = repo;
        }
        public void DeleteRefreshToken(string username, string refreshToken)
        {
            this.repo.DeleteRefreshToken(username, refreshToken);
        }

        public string GetRefreshToken(string username)
        {
            return this.repo.GetRefreshToken(username);
        }

        public void SaveRefreshToken(string username, string newRefreshToken)
        {
            this.repo.SaveRefreshToken(username, newRefreshToken);
        }
    }
}
