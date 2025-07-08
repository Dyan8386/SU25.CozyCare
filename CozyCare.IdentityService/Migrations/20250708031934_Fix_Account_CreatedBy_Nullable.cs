using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CozyCare.IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Account_CreatedBy_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    accountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdBy = table.Column<int>(type: "int", nullable: true),
                    updatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accounts__F267251E562754A3", x => x.accountId);
                    table.ForeignKey(
                        name: "FK_Accounts_CreatedBy",
                        column: x => x.createdBy,
                        principalTable: "Accounts",
                        principalColumn: "accountId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "roleId");
                    table.ForeignKey(
                        name: "FK_Accounts_Status",
                        column: x => x.statusId,
                        principalTable: "AccountStatuses",
                        principalColumn: "statusId");
                    table.ForeignKey(
                        name: "FK_Accounts_UpdatedBy",
                        column: x => x.updatedBy,
                        principalTable: "Accounts",
                        principalColumn: "accountId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_createdBy",
                table: "Accounts",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_roleId",
                table: "Accounts",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_statusId",
                table: "Accounts",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_updatedBy",
                table: "Accounts",
                column: "updatedBy");

            migrationBuilder.CreateIndex(
                name: "UQ__Accounts__AB6E6164BE93AEE0",
                table: "Accounts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__AccountS__6A50C21291DAFC7D",
                table: "AccountStatuses",
                column: "statusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__B1947861DD1E251C",
                table: "Roles",
                column: "roleName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

        }
    }
}
