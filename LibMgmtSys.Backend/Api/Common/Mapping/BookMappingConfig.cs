using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand;
using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;
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
        .Map(dest => dest.AuthorIds, src => src.AuthorIds.Select(id => AuthorId.Create(Guid.Parse(id))))
        .Map(dest => dest.Year, src => src.Year)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.GenreIds, src => src.GenreIds.Select(id => GenreId.Create(Guid.Parse(id))))
        .Map(dest => dest.Image, src => src.Image)
        .Map(dest => dest.BorrowingPeriod, src => src.BorrowingPeriod)
        .Map(dest => dest.Quantity, src => src.Quantity);
      
      config.NewConfig<(UpdateBookRequest Request, string Id), UpdateBookCommand>()
        .Map(dest => dest.Id, src => Guid.Parse(src.Id))
        .Map(dest => dest, src  => src.Request);
        
      config.NewConfig<Book, BookResponse>()
        .Map(dest => dest.Id, src => src.Id.Value.ToString())
        .Map(dest => dest.Title, src => src.Title)
        .Map(dest => dest.AuthorNames, src => src.Authors.Select(author => author.Name))
        .Map(dest => dest.Isbn, src => src.Isbn)
        .Map(dest => dest.Publisher, src => src.Publisher)
        .Map(dest => dest.Year, src => src.Year)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.GenreNames, src => src.Genres.Select(genre => genre.Name))
        .Map(dest => dest.AverageRating, src => src.AverageRating.Value)
        .Map(dest => dest.Image, src => src.Image)
        .Map(dest => dest.BorrowingPeriod, src => src.BorrowingPeriod)
        .Map(dest => dest.Quantity, src => src.Quantity);
      
      config.NewConfig<Guid, BookId>()
        .Map(dest => dest.Value, src => src);
      
      config.NewConfig<BookId, Guid>()
        .Map(dest => dest, src => src.Value);
      
      /*config.NewConfig<string, AuthorId>()
        .Map(dest => dest.Value.ToString(), src => src);
      
      config.NewConfig<AuthorId, string>()
        .Map(dest => dest, src => src.Value.ToString());
      
      config.NewConfig<Guid, GenreId>()
        .Map(dest => dest.Value, src => src);
      
      config.NewConfig<GenreId, Guid>()
        .Map(dest => dest, src => src.Value);*/
      
      /*config.NewConfig<Guid, AuthorId>()
        .Map(dest => dest.Value, src => src);
      
      config.NewConfig<AuthorId, Guid>()
        .Map(dest => dest, src => src.Value);*/
      
      /*config.NewConfig<Guid, GenreId>()
        .Map(dest => dest.Value, src => src);
      
      config.NewConfig<GenreId, Guid>()
        .Map(dest => dest, src => src.Value);*/

      // TODO: Add mapping for BookReview
    }
  }

}