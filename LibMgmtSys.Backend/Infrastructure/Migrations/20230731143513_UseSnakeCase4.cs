using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseSnakeCase4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bills_customers_customer_id",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "fk_book_reviews_books_book_id1",
                table: "BookReviews");

            migrationBuilder.DropForeignKey(
                name: "fk_loans_customers_customer_id1",
                table: "Loans");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "loans");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "genres");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "customers");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "books");

            migrationBuilder.RenameTable(
                name: "Bills",
                newName: "bills");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "authors");

            migrationBuilder.RenameTable(
                name: "BookReviews",
                newName: "book_reviews");

            migrationBuilder.AddForeignKey(
                name: "fk_bills_customers_customer_temp_id",
                table: "bills",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_book_reviews_books_book_temp_id",
                table: "book_reviews",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_loans_customers_customer_temp_id1",
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
                name: "fk_bills_customers_customer_temp_id",
                table: "bills");

            migrationBuilder.DropForeignKey(
                name: "fk_book_reviews_books_book_temp_id",
                table: "book_reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_loans_customers_customer_temp_id1",
                table: "loans");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "loans",
                newName: "Loans");

            migrationBuilder.RenameTable(
                name: "genres",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "books",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "bills",
                newName: "Bills");

            migrationBuilder.RenameTable(
                name: "authors",
                newName: "Authors");

            migrationBuilder.RenameTable(
                name: "book_reviews",
                newName: "BookReviews");

            migrationBuilder.AddForeignKey(
                name: "fk_bills_customers_customer_id",
                table: "Bills",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_book_reviews_books_book_id1",
                table: "BookReviews",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_loans_customers_customer_id1",
                table: "Loans",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
