using Contracts.Loans;
using LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using Mapster;

namespace LibMgmtSys.Backend.Api.Common.Mapping
{
    public class LoanMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateLoanRequest, CreateLoanCommand>()
                //.Map(dest => dest.BookIds, src => src.BookIds)
                //.Map(dest => dest.LoanedAt, src => src.LoanedAt);
                //.Map(dest => dest.CustomerId, src => src.CustomerId);
                .Map(dest => dest, src => src);
            
            config.NewConfig<Loan, LoanResponse>()
                .Map(dest => dest.LoanId, src => src.Id.Value.ToString())
                .Map(dest => dest.BookId, src => src.BookId.Value.ToString())
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.CustomerId, src => src.CustomerId.Value.ToString())
                .Map(dest => dest.CustomerEmail, src => src.Customer.Email)
                .Map(dest => dest.LoanedAt, src => src.LoanedAt)
                .Map(dest => dest.ReturnedAt, src => src.ReturnedAt)
                .Map(dest => dest.DueDate, src => src.DueDate)
                .Map(dest => dest.ReturnedAt, src => src.ReturnedAt);
            
            /*config.NewConfig<Guid, CustomerId>()
                .MapWith(id => CustomerId.Create(id));
      
            config.NewConfig<CustomerId, Guid>()
                .Map(dest => dest, src => src.Value);
                
            
            config.NewConfig<Guid, BookId>()
                .Map(dest => dest.Value, src => src);
      
            config.NewConfig<BookId, Guid>()
                .Map(dest => dest, src => src.Value);*/
            
            config.NewConfig<Guid, BookId>()
                .MapWith(id => BookId.Create(id));
            
            config.NewConfig<BookId, Guid>()
                .MapWith(id => id.Value);
        }
    }
}