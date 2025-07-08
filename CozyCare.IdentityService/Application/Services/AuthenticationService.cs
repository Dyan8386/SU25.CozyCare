using AutoMapper;
using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.IdentityService.Domain.Entities;
using CozyCare.IdentityService.Infrastructure;
using CozyCare.SharedKernel.Base;
using CozyCare.SharedKernel.Store;
using CozyCare.SharedKernel.Utils;
using CozyCare.ViewModels.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly JwtSettings _jwtSettings;


        public AuthenticationService(IIdentityUnitOfWork uow, IMapper mapper, IConfiguration configuration, IOptions<JwtSettings> jwtOptions)
        {
            _uow = uow;
            _mapper = mapper;
            _configuration = configuration;
            _jwtSettings = jwtOptions?.Value ?? throw new InvalidOperationException("JWT Key is not configured.");
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
            var keyString = _jwtSettings.Key ?? throw new InvalidOperationException("JWT Key is not configured.");
            var issuer = _jwtSettings.Issuer ?? throw new InvalidOperationException("JWT Issuer is not configured.");
            var audience = _jwtSettings.Audience ?? throw new InvalidOperationException("JWT Audience is not configured.");

            var key = Encoding.UTF8.GetBytes(keyString);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.email),
                new Claim(ClaimTypes.NameIdentifier, account.accountId.ToString()),
                new Claim(ClaimTypes.Email, account.email ?? ""),
                new Claim("FullName", account.fullName ?? ""),
                new Claim("Avatar", account.avatar ?? ""),
                new Claim(ClaimTypes.Role, account.roleId.ToString()),
                new Claim("RoleName", account.role?.roleName ?? ""),
                new Claim("StatusId", account.statusId.ToString()),
                new Claim("StatusName", account.status?.statusName ?? "")
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
