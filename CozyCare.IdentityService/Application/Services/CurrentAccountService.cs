using CozyCare.IdentityService.Application.Interfaces;
using CozyCare.ViewModels.DTOs;
using System.Security.Claims;

namespace CozyCare.IdentityService.Application.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentAccountService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public CurrentAccountDto? GetCurrentAccount()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
                return null;

            return new CurrentAccountDto
            {
                AccountId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
                Email = user.FindFirstValue(ClaimTypes.Email) ?? "",
                FullName = user.FindFirstValue("FullName"),
                Avatar = user.FindFirstValue("Avatar"),
                RoleId = int.Parse(user.FindFirstValue(ClaimTypes.Role) ?? "0"),
                RoleName = user.FindFirstValue("RoleName") ?? "",
                StatusId = int.Parse(user.FindFirstValue("StatusId") ?? "0"),
                StatusName = user.FindFirstValue("StatusName") ?? ""
            };
        }
    }
}
