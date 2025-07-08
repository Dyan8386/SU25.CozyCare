using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.SharedKernel.Store
{
    public class TokenAccessor : ITokenAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetAccessToken()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.User?.Identity?.IsAuthenticated != true)
                return null;

            // Nếu bạn lưu token trong header Authorization hoặc trong Claim, lấy từ đó
            var token = context.Request.Headers["Authorization"].ToString();
            return token?.Replace("Bearer ", "");
        }
    }
}
