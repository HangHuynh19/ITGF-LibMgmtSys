using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGenreSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_loans_customers_customer_temp_id1",
                table: "loans");

            migrationBuilder.DropColumn(
                name: "description",
                table: "genres");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "book_reviews",
                newName: "customer_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "book_reviews",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "ix_book_reviews_customer_id",
                table: "book_reviews",
                column: "customer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_book_reviews_customers_customer_temp_id1",
                table: "book_reviews",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_loans_customers_customer_temp_id2",
                table: "loans",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_book_reviews_customers_customer_temp_id1",
                table: "book_reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_loans_customers_customer_temp_id2",
                table: "loans");

            migrationBuilder.DropIndex(
                name: "ix_book_reviews_customer_id",
                table: "book_reviews");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "book_reviews",
                newName: "user_id");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "genres",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "book_reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_loans_customers_customer_temp_id1",
                table: "loans",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
