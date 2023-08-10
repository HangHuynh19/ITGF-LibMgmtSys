using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Configuration;

using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.BillAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookReviewAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate;

using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BillAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookReviewAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.Models;
using LibMgmtSys.Backend.Domain.Common.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.Enum;

using LibMgmtSys.Backend.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibMgmtSys.Backend.Infrastructure.Persistence
{
    public class LibMgmtSysDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public LibMgmtSysDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<BookReview> BookReviews { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => BookId.Create(e));
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Isbn).IsRequired();
                entity.Property(e => e.Publisher).IsRequired();
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.OwnsOne(e => e.AverageRating, ab =>
                {
                    ab.Property(e => e.Value).HasColumnName("average_rating");
                    ab.Property(e => e.NumberOfRatings).HasColumnName("number_of_ratings");
                });
                entity.Property(e => e.Image).IsRequired();
                entity.Property(e => e.BorrowingPeriod).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.HasMany(e => e.Authors).WithMany(e => e.Books);
                entity.HasMany(e => e.Genres).WithMany(e => e.Books);
                entity.HasMany(e => e.BookReviews).WithOne(e => e.Book);
                entity.HasMany(e => e.Loans).WithOne(e => e.Book);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => AuthorId.Create(e));
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Biography).IsRequired();
                entity.HasMany(e => e.Books).WithMany(e => e.Authors).UsingEntity<Dictionary<string, object>>(
                    "book_author",
                    e => e.HasOne<Book>().WithMany().HasForeignKey("bookId").OnDelete(DeleteBehavior.Cascade),
                    e => e.HasOne<Author>().WithMany().HasForeignKey("authorId").OnDelete(DeleteBehavior.Cascade),
                    e =>
                    {
                        e.HasKey("bookId", "authorId");
                        e.HasIndex("authorId");
                    }
                );
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => GenreId.Create(e));
                entity.Property(e => e.Name).IsRequired();
                entity.HasMany(e => e.Books).WithMany(e => e.Genres);
            });

            modelBuilder.Entity<BookReview>(entity =>
            {
                entity.ToTable("book_reviews");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => BookReviewId.Create(e));
                entity.Property(e => e.Comment).IsRequired();
                entity.Property(e => e.Rating).HasConversion(e => e.Value, e => Rating.Create(e));
                entity.Property(e => e.BookId).HasConversion(e => e.Value, e => BookId.Create(e));
                entity.Property(e => e.CustomerId).HasConversion(e => e.Value, e => CustomerId.Create(e));
                entity.HasOne(e => e.Book).WithMany(e => e.BookReviews);
            });

            //modelBuilder.HasPostgresEnum<Role>();
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => UserId.Create(e));
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Role).HasConversion(
                    e => e.Value.ToString(),
                    value => Role.FromValue(int.Parse(value)));
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => CustomerId.Create(e));
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.ProfileImage).IsRequired();
                entity.Property(e => e.UserId).HasConversion(e => e.Value, e => UserId.Create(e));
                entity.HasMany(e => e.Loans).WithOne(e => e.Customer).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.Bills).WithOne(e => e.Customer).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("loans");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => LoanId.Create(e));
                entity.Property(e => e.BookId).HasConversion(e => e.Value, e => BookId.Create(e));
                entity.Property(e => e.CustomerId);
                entity.HasOne(e => e.Customer).WithMany(e => e.Loans).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Book).WithMany(e => e.Loans);
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bills");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever().HasConversion(e => e.Value, e => BillId.Create(e));
                entity.Property(e => e.LoanId).ValueGeneratedNever().HasConversion(e => e.Value, e => LoanId.Create(e));
                entity.Property(e => e.Amount).IsRequired();
                entity.HasOne(e => e.Customer).WithMany(e => e.Bills);
            });
        }
    }
}
