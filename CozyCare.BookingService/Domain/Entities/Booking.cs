﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CozyCare.BookingService.Domain.Entities;

[Index("bookingNumber", Name = "UQ__Bookings__CB36BB4D2E7005E0", IsUnique = true)]
public partial class Booking
{
    [Key]
    public int bookingId { get; set; }

    [Required]
    [StringLength(50)]
    public string bookingNumber { get; set; }

    public int customerId { get; set; }

    [StringLength(50)]
    public string promotionCode { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? bookingDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime deadline { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal totalAmount { get; set; }

    [StringLength(500)]
    public string notes { get; set; }

    public int bookingStatusId { get; set; }

    public int paymentStatusId { get; set; }

	[StringLength(400)] // Adjust the length as needed
	public string address { get; set; } // Add the Address field

	[InverseProperty("booking")]
    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    [ForeignKey("bookingStatusId")]
    [InverseProperty("Bookings")]
    public virtual BookingStatus bookingStatus { get; set; }
}