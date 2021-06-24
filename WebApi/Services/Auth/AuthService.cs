using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Models.Auth;
using WebApi.Utilities;

namespace WebApi.Services.Auth
{
    public class AuthService:IAuthService
    {
        private AppDbContext _context;
        private IJwtUtils _jwtUtils;

        public AuthService(AppDbContext context,IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, string ipAddress)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == request.Username);

            if (user != null)
            {
                if (PasswordHash.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    // authentication successful so generate jwt and refresh tokens
                    var jwtToken = _jwtUtils.GenerateJwtToken(user);
                    var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
                    user.RefreshTokens.Add(refreshToken);

                    // remove old refresh tokens from user
                    _jwtUtils.removeOldRefreshTokens(user);

                    // save changes to db
                    _context.Update(user);
                    _context.SaveChanges();

                    response.Id = user.Id;
                    response.Username = user.Username;
                    response.JwtToken = jwtToken;
                    response.RefreshToken = refreshToken.Token;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid Password";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Invalid User";
            }
            return response;
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            AuthenticateResponse response = new AuthenticateResponse();
            var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user != null)
            {
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
                // return null if token is no longer active
                if (refreshToken.IsActive)
                {
                    if (!refreshToken.IsRevoked)
                    {
                        // replace old refresh token with a new one (rotate token)
                        var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
                        user.RefreshTokens.Add(newRefreshToken);

                        // remove old refresh tokens from user
                        _jwtUtils.removeOldRefreshTokens(user);

                        // save changes to db
                        _context.Update(user);
                        _context.SaveChanges();

                        // generate new jwt
                        var jwtToken = _jwtUtils.GenerateJwtToken(user);
                        response.Id = user.Id;
                        response.Username = user.Username;
                        response.JwtToken = jwtToken;
                        response.RefreshToken = newRefreshToken.Token;
                    }
                    //else
                    //{
                    //    // revoke all descendant tokens in case this token has been compromised
                    //    _jwtUtils.revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                    //    _context.Update(user);
                    //    _context.SaveChanges();
                    //    response.Success = false;
                    //    response.Message = "Revoked Token";
                    //}
                }
                else
                {
                    response.Success = false;
                    response.Message = "Inactive Token";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Invalid Token";
            }
           
            return response;

        }
        public async Task<bool> RevokeToken(string token, string ipAddress)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            _jwtUtils.revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _context.Update(user);
            _context.SaveChanges();

            return true;
        }
       
        public RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            _jwtUtils.revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

    }
}
