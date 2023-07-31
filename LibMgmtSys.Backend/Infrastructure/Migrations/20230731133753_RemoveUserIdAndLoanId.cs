using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdAndLoanId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanId");

            migrationBuilder.DropTable(
                name: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanId",
                columns: table => new
                {
                    Value = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserId",
                columns: table => new
                {
                    Value = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
