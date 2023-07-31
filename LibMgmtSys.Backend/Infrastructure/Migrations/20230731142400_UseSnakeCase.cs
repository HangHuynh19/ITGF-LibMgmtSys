using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Books_BooksId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genres_GenresId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookReviews_Books_BookId",
                table: "BookReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Customers_CustomerId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookReviews",
                table: "BookReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bills",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorBook",
                table: "AuthorBook");

            migrationBuilder.RenameTable(
                name: "BookGenre",
                newName: "book_genre");

            migrationBuilder.RenameTable(
                name: "AuthorBook",
                newName: "author_book");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Loans",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "Loans",
                newName: "returned_at");

            migrationBuilder.RenameColumn(
                name: "LoanedAt",
                table: "Loans",
                newName: "loaned_at");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Loans",
                newName: "due_date");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Loans",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Loans",
                newName: "book_id");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_CustomerId",
                table: "Loans",
                newName: "ix_loans_customer_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Genres",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Customers",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "Customers",
                newName: "profile_image");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customers",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Books",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Books",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "Books",
                newName: "publisher");

            migrationBuilder.RenameColumn(
                name: "Isbn",
                table: "Books",
                newName: "isbn");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Books",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Books",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Books",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BorrowingPeriod",
                table: "Books",
                newName: "borrowing_period");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "BookReviews",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "BookReviews",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BookReviews",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookReviews",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "BookReviews",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "BookReviews",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookReviews",
                newName: "book_id");

            migrationBuilder.RenameIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                newName: "ix_book_reviews_book_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Bills",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bills",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PaidAt",
                table: "Bills",
                newName: "paid_at");

            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "Bills",
                newName: "loan_id");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Bills",
                newName: "due_date");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Bills",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Bills",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                newName: "ix_bills_customer_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Authors",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Biography",
                table: "Authors",
                newName: "biography");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Authors",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "book_genre",
                newName: "genres_id");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "book_genre",
                newName: "books_id");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenre_GenresId",
                table: "book_genre",
                newName: "ix_book_genre_genres_id");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "author_book",
                newName: "books_id");

            migrationBuilder.RenameColumn(
                name: "AuthorsId",
                table: "author_book",
                newName: "authors_id");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_BooksId",
                table: "author_book",
                newName: "ix_author_book_books_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "Users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_loans",
                table: "Loans",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_genres",
                table: "Genres",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_customers",
                table: "Customers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_books",
                table: "Books",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_book_reviews",
                table: "BookReviews",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_bills",
                table: "Bills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_authors",
                table: "Authors",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_book_genre",
                table: "book_genre",
                columns: new[] { "books_id", "genres_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_author_book",
                table: "author_book",
                columns: new[] { "authors_id", "books_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_author_book_authors_authors_temp_id",
                table: "author_book",
                column: "authors_id",
                principalTable: "Authors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_author_book_books_books_temp_id1",
                table: "author_book",
                column: "books_id",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_bills_customers_customer_id",
                table: "Bills",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_book_genre_books_books_temp_id2",
                table: "book_genre",
                column: "books_id",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_book_genre_genres_genres_temp_id",
                table: "book_genre",
                column: "genres_id",
                principalTable: "Genres",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_author_book_authors_authors_temp_id",
                table: "author_book");

            migrationBuilder.DropForeignKey(
                name: "fk_author_book_books_books_temp_id1",
                table: "author_book");

            migrationBuilder.DropForeignKey(
                name: "fk_bills_customers_customer_id",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "fk_book_genre_books_books_temp_id2",
                table: "book_genre");

            migrationBuilder.DropForeignKey(
                name: "fk_book_genre_genres_genres_temp_id",
                table: "book_genre");

            migrationBuilder.DropForeignKey(
                name: "fk_book_reviews_books_book_id1",
                table: "BookReviews");

            migrationBuilder.DropForeignKey(
                name: "fk_loans_customers_customer_id1",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_loans",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "pk_genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "pk_customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "pk_book_reviews",
                table: "BookReviews");

            migrationBuilder.DropPrimaryKey(
                name: "pk_bills",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_authors",
                table: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "pk_book_genre",
                table: "book_genre");

            migrationBuilder.DropPrimaryKey(
                name: "pk_author_book",
                table: "author_book");

            migrationBuilder.RenameTable(
                name: "book_genre",
                newName: "BookGenre");

            migrationBuilder.RenameTable(
                name: "author_book",
                newName: "AuthorBook");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Loans",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "returned_at",
                table: "Loans",
                newName: "ReturnedAt");

            migrationBuilder.RenameColumn(
                name: "loaned_at",
                table: "Loans",
                newName: "LoanedAt");

            migrationBuilder.RenameColumn(
                name: "due_date",
                table: "Loans",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Loans",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "Loans",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "ix_loans_customer_id",
                table: "Loans",
                newName: "IX_Loans_CustomerId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Genres",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Genres",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Genres",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Customers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "profile_image",
                table: "Customers",
                newName: "ProfileImage");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Customers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "year",
                table: "Books",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Books",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "publisher",
                table: "Books",
                newName: "Publisher");

            migrationBuilder.RenameColumn(
                name: "isbn",
                table: "Books",
                newName: "Isbn");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Books",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Books",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Books",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Books",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "borrowing_period",
                table: "Books",
                newName: "BorrowingPeriod");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "BookReviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "BookReviews",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "BookReviews",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "BookReviews",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "BookReviews",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "BookReviews",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "book_id",
                table: "BookReviews",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "ix_book_reviews_book_id",
                table: "BookReviews",
                newName: "IX_BookReviews_BookId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Bills",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bills",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "paid_at",
                table: "Bills",
                newName: "PaidAt");

            migrationBuilder.RenameColumn(
                name: "loan_id",
                table: "Bills",
                newName: "LoanId");

            migrationBuilder.RenameColumn(
                name: "due_date",
                table: "Bills",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Bills",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Bills",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_bills_customer_id",
                table: "Bills",
                newName: "IX_Bills_CustomerId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Authors",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "biography",
                table: "Authors",
                newName: "Biography");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Authors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "genres_id",
                table: "BookGenre",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "books_id",
                table: "BookGenre",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "ix_book_genre_genres_id",
                table: "BookGenre",
                newName: "IX_BookGenre_GenresId");

            migrationBuilder.RenameColumn(
                name: "books_id",
                table: "AuthorBook",
                newName: "BooksId");

            migrationBuilder.RenameColumn(
                name: "authors_id",
                table: "AuthorBook",
                newName: "AuthorsId");

            migrationBuilder.RenameIndex(
                name: "ix_author_book_books_id",
                table: "AuthorBook",
                newName: "IX_AuthorBook_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookReviews",
                table: "BookReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bills",
                table: "Bills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenre",
                table: "BookGenre",
                columns: new[] { "BooksId", "GenresId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorBook",
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Authors_AuthorsId",
                table: "AuthorBook",
                column: "AuthorsId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Books_BooksId",
                table: "AuthorBook",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genres_GenresId",
                table: "BookGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookReviews_Books_BookId",
                table: "BookReviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Customers_CustomerId",
                table: "Loans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
