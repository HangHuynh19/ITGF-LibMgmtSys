using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibMgmtSys.Backend.Domain
{
  public class AuthorBookEntity
  {
    public BookId BookId { get; set; }
    public AuthorId AuthorId { get; set; }
    public Book Book { get; set; } = null!;
    public Author Author { get; set; } = null!;
  }
}