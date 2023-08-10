using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand;
using LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand;
using LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery;
using LibMgmtSys.Backend.Application.Books.Queries.GetBookByIdQuery;
using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibMmgtSys.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public BooksController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
        {
            var createBookCommand = _mapper.Map<CreateBookCommand>(request);
            var createBookResult = await _mediator.Send(createBookCommand);
            
            return createBookResult.Match(
                book => Ok(_mapper.Map<BookResponse>(book)),
                errors => Problem(errors));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var getAllBooksQuery = new GetAllBooksWithPaginationQuery(pageNumber, pageSize);
            var getAllBooksResult = await _mediator.Send(getAllBooksQuery);

            return getAllBooksResult.Match(
                books => Ok(books.Select(result => _mapper.Map<BookResponse>(result))),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] string id)
        {
            var getBookByIdQuery = new GetBookByIdQuery(BookId.Create(Guid.Parse(id)));
            var getBookByIdResult = await _mediator.Send(getBookByIdQuery);
            
            return getBookByIdResult.Match(
                book => Ok(_mapper.Map<BookResponse>(book)),
                errors => Problem(errors));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] string id, [FromBody] UpdateBookRequest request)
        {
            var updateBookCommand = _mapper.Map<UpdateBookCommand>((request, id));
            var updateBookResult = await _mediator.Send(updateBookCommand);
            
            return updateBookResult.Match(
                book => Ok(_mapper.Map<BookResponse>(book)),
                errors => Problem(errors));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string id)
        {
            var deleteBookCommand = new DeleteBookCommand(BookId.Create(Guid.Parse(id)));
            var deleteBookResult = await _mediator.Send(deleteBookCommand);
            
            return deleteBookResult.Match(
                book => Ok(_mapper.Map<BookResponse>(book)),
                errors => Problem(errors));
        }
    }
}