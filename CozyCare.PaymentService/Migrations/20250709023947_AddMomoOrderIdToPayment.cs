﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyCare.PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class AddMomoOrderIdToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "momoOrderId",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "momoOrderId",
                table: "Payments");
        }
    }
}
