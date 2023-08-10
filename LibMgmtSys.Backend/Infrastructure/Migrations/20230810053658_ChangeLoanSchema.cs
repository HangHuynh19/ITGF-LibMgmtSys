using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLoanSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_book_genre_books_books_temp_id2",
                table: "book_genre");

            migrationBuilder.CreateIndex(
                name: "ix_loans_book_id",
                table: "loans",
                column: "book_id");

            migrationBuilder.AddForeignKey(
                name: "fk_book_genre_books_books_temp_id3",
                table: "book_genre",
                column: "books_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_loans_books_book_temp_id1",
                table: "loans",
                column: "book_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_book_genre_books_books_temp_id3",
                table: "book_genre");

            migrationBuilder.DropForeignKey(
                name: "fk_loans_books_book_temp_id1",
                table: "loans");

            migrationBuilder.DropIndex(
                name: "ix_loans_book_id",
                table: "loans");

            migrationBuilder.AddForeignKey(
                name: "fk_book_genre_books_books_temp_id2",
                table: "book_genre",
                column: "books_id",
                principalTable: "books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
