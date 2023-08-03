using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Contracts.Books;
using MapsterMapper;
using MediatR;
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
    }
}