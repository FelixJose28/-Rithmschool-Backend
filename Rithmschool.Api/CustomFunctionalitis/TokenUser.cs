using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rithmschool.Core.Entities;
using Rithmschool.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rithmschool.Api.CustomFunctionalitis
{
    public class TokenUser
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConfiguration _configuration;
        public TokenUser(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signinCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signinCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddHours(16)
            );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public async Task<(bool, User)> IsValidUser(UserLoginCustom login)
        {
            var user = await _unitOfWork.authenticationRepository.Loggin(login);
            return (user != null, user);
        }
    }
}
