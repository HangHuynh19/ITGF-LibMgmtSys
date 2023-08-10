﻿// <auto-generated />
using System;
using LibMgmtSys.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(LibMgmtSysDbContext))]
    [Migration("20230810130052_FixLoanMapping7")]
    partial class FixLoanMapping7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<Guid>("BooksId")
                        .HasColumnType("uuid")
                        .HasColumnName("books_id");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("uuid")
                        .HasColumnName("genres_id");

                    b.HasKey("BooksId", "GenresId")
                        .HasName("pk_book_genre");

                    b.HasIndex("GenresId")
                        .HasDatabaseName("ix_book_genre_genres_id");

                    b.ToTable("book_genre", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.AuthorAggregate.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("biography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BillAggregate.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("due_date");

                    b.Property<Guid>("LoanId")
                        .HasColumnType("uuid")
                        .HasColumnName("loan_id");

                    b.Property<DateTime?>("PaidAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("paid_at");

                    b.HasKey("Id")
                        .HasName("pk_bills");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_bills_customer_id");

                    b.ToTable("bills", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BookAggregate.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("BorrowingPeriod")
                        .HasColumnType("integer")
                        .HasColumnName("borrowing_period");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("isbn");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("publisher");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_books");

                    b.ToTable("books", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BookReviewAggregate.BookReview", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<int>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_book_reviews");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_book_reviews_book_id");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_book_reviews_customer_id");

                    b.ToTable("book_reviews", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("profile_image");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_customers");

                    b.ToTable("customers", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.GenreAggregate.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_genres");

                    b.ToTable("genres", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.LoanAggregate.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("due_date");

                    b.Property<DateTime>("LoanedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("loaned_at");

                    b.Property<DateTime?>("ReturnedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("returned_at");

                    b.HasKey("Id")
                        .HasName("pk_loans");

                    b.HasIndex("BookId")
                        .HasDatabaseName("ix_loans_book_id");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_loans_customer_id");

                    b.ToTable("loans", (string)null);
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("book_author", b =>
                {
                    b.Property<Guid>("bookId")
                        .HasColumnType("uuid")
                        .HasColumnName("book_id");

                    b.Property<Guid>("authorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.HasKey("bookId", "authorId")
                        .HasName("pk_book_author");

                    b.HasIndex("authorId")
                        .HasDatabaseName("ix_book_author_author_id");

                    b.ToTable("book_author", (string)null);
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("LibMgmtSys.Backend.Domain.BookAggregate.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_genre_books_books_temp_id3");

                    b.HasOne("LibMgmtSys.Backend.Domain.GenreAggregate.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_genre_genres_genres_temp_id");
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BillAggregate.Bill", b =>
                {
                    b.HasOne("LibMgmtSys.Backend.Domain.CustomerAggregate.Customer", "Customer")
                        .WithMany("Bills")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_bills_customers_customer_temp_id");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BookAggregate.Book", b =>
                {
                    b.OwnsOne("LibMgmtSys.Backend.Domain.Common.ValueObjects.AverageRating", "AverageRating", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<int>("NumberOfRatings")
                                .HasColumnType("integer")
                                .HasColumnName("number_of_ratings");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("average_rating");

                            b1.HasKey("BookId");

                            b1.ToTable("books");

                            b1.WithOwner()
                                .HasForeignKey("BookId")
                                .HasConstraintName("fk_books_books_id");
                        });

                    b.Navigation("AverageRating")
                        .IsRequired();
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BookReviewAggregate.BookReview", b =>
                {
                    b.HasOne("LibMgmtSys.Backend.Domain.BookAggregate.Book", "Book")
                        .WithMany("BookReviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_reviews_books_book_temp_id");

                    b.HasOne("LibMgmtSys.Backend.Domain.CustomerAggregate.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_reviews_customers_customer_temp_id1");

                    b.Navigation("Book");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.LoanAggregate.Loan", b =>
                {
                    b.HasOne("LibMgmtSys.Backend.Domain.BookAggregate.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_loans_books_book_temp_id1");

                    b.HasOne("LibMgmtSys.Backend.Domain.CustomerAggregate.Customer", "Customer")
                        .WithMany("Loans")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_loans_customers_customer_temp_id2");

                    b.Navigation("Book");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("book_author", b =>
                {
                    b.HasOne("LibMgmtSys.Backend.Domain.AuthorAggregate.Author", null)
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_author_authors_author_id");

                    b.HasOne("LibMgmtSys.Backend.Domain.BookAggregate.Book", null)
                        .WithMany()
                        .HasForeignKey("bookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_book_author_books_book_id");
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.BookAggregate.Book", b =>
                {
                    b.Navigation("BookReviews");

                    b.Navigation("Loans");
                });

            modelBuilder.Entity("LibMgmtSys.Backend.Domain.CustomerAggregate.Customer", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}