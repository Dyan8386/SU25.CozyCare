using AutoMapper;
using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CozyCare.IdentityService.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IIdentityUnitOfWork uow, IMapper mapper, IConfiguration configuration)
        {
            _uow = uow;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseResponse<LoginResponseDto>> AuthenticateAsync(LoginRequestDto request)
        {
            var account = (await _uow.Accounts
                .SearchAsync(a => a.email == request.Input || a.phone == request.Input))
                .FirstOrDefault();

            if (account == null || !PasswordHelper.VerifyPassword(request.Password, account.password))
                return BaseResponse<LoginResponseDto>.UnauthorizeResponse("Invalid email/phone or password");

            var token = GenerateJwtToken(account);
            var response = new LoginResponseDto
            {
                AccessToken = token,
                Expires = DateTime.UtcNow.AddHours(1),
                Account = _mapper.Map<AccountDto>(account)
            };

            return BaseResponse<LoginResponseDto>.OkResponse(response);
        }

        private string GenerateJwtToken(Account account)
        {
            // read and validate
            var keyString = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is not configured.");
            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT Audience is not configured.");

            var key = Encoding.UTF8.GetBytes(keyString);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.email),
                new Claim("accountId", account.accountId.ToString()),
                new Claim("roleId", account.roleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
