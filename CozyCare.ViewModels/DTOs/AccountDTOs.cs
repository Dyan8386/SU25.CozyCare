using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.ViewModels.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }

    public class CreateAccountRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int? CreatedBy { get; set; }
    }

    public class UpdateAccountRequestDto
    {
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? RoleId { get; set; }
        public int? StatusId { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
