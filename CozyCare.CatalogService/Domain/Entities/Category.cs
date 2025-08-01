﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.CatalogService.Domain.Entities;

public partial class Category
{
    [Key]
    public int categoryId { get; set; }

    [Required]
    [StringLength(255)]
    public string categoryName { get; set; }

    [StringLength(500)]
    public string description { get; set; }

    [StringLength(255)]
    public string image { get; set; }

    public bool? isActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? createdDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? updatedDate { get; set; }

    [InverseProperty("category")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}