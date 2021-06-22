using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
        public void removeOldRefreshTokens(User user);
        public void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason);
        public void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null);
    }
}
