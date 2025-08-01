﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.BookingService.Domain.Entities;

[Index("statusName", Name = "UQ__BookingS__6A50C21251788D40", IsUnique = true)]
public partial class BookingStatus
{
    [Key]
    public int statusId { get; set; }

    [Required]
    [StringLength(50)]
    public string statusName { get; set; }

    [InverseProperty("bookingStatus")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}