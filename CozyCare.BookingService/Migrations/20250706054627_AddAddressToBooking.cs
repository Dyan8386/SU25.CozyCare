using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyCare.BookingService.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<string>(
				name: "address",
				table: "Bookings",
				type: "nvarchar(400)",
				maxLength: 400,
				nullable: true);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
				name: "address",
				table: "Bookings");
		}
    }
}
