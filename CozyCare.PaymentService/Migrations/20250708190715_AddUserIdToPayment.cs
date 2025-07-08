﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyCare.PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
              name: "userId",
              table: "Payments",
              type: "int",
              nullable: false,
              defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "userId",
               table: "Payments"
            );
        }
    }
}
