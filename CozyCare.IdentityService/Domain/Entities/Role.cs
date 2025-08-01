﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.IdentityService.Domain.Entities;

[Index("roleName", Name = "UQ__Roles__B1947861DD1E251C", IsUnique = true)]
public partial class Role
{
    [Key]
    public int roleId { get; set; }

    [Required]
    [StringLength(50)]
    public string roleName { get; set; }

    [InverseProperty("role")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}