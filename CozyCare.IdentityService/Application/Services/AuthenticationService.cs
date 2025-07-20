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
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            IIdentityUnitOfWork uow,
            IMapper mapper,
            IConfiguration configuration,
            IOptions<JwtSettings> jwtOptions)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtSettings = jwtOptions.Value;
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
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                Account = _mapper.Map<AccountDto>(account)
            };

            return BaseResponse<LoginResponseDto>.OkResponse(response);
        }

        public async Task<BaseResponse<AccountDto>> RegisterAsync(RegisterRequestDto request)
        {
            // TODO: implement registration (verify email/phone, OTP)
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<string>> ConfirmEmailAsync(string email, string token)
        {
            // TODO: confirm email
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<string>> SendOtpAsync(string phone)
        {
            // TODO: send OTP
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<string>> VerifyOtpAsync(string phone, string otp)
        {
            // TODO: verify OTP
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<string>> ResetPasswordAsync(ResetPasswordRequestDto request)
        {
            // TODO: reset password
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(Account account)
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.email),
                new Claim(ClaimTypes.NameIdentifier, account.accountId.ToString()),
                new Claim(ClaimTypes.Email, account.email ?? ""),
                new Claim("FullName", account.fullName ?? ""),
                new Claim(ClaimTypes.Role, account.roleId.ToString())
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
