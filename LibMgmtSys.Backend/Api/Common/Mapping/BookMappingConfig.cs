using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
  public class BookMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<CreateBookRequest, CreateBookCommand>()
        .Map(dest => dest.Title, src => src.Title)
        .Map(dest => dest.Isbn, src => src.Isbn)
        .Map(dest => dest.Publisher, src => src.Publisher)
        //.Map(dest => dest.AuthorIds, src => src.AuthorIds)
        .Map(dest => dest.Year, src => src.Year)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.Image, src => src.Image)
        .Map(dest => dest.BorrowingPeriod, src => src.BorrowingPeriod)
        .Map(dest => dest.Quantity, src => src.Quantity);
        
      config.NewConfig<Book, BookResponse>()
        .Map(dest => dest.Id, src => src.Id.Value.ToString())
        .Map(dest => dest.Title, src => src.Title)
        .Map(dest => dest.AuthorNames, src => src.Authors.Select(author => author.Name))
        .Map(dest => dest.Isbn, src => src.Isbn)
        .Map(dest => dest.Publisher, src => src.Publisher)
        .Map(dest => dest.Year, src => src.Year)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
        .Map(dest => dest.Image, src => src.Image)
        .Map(dest => dest.BorrowingPeriod, src => src.BorrowingPeriod)
        .Map(dest => dest.Quantity, src => src.Quantity)
        .Map(dest => dest.CreatedAt, src => src.CreatedAt)
        .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);
      
      config.NewConfig<Guid, AuthorId>()
        .Map(dest => dest.Value, src => src);
      
      config.NewConfig<AuthorId, Guid>()
        .Map(dest => dest, src => src.Value);
      
      // TODO: Add mapping for Genre and BookReview
    }
  }

}