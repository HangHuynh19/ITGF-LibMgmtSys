using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Contracts.Books;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibMmgtSys.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
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
            var response = _mapper.Map<BookResponse>(createBookResult);
            return Ok(response);
        }
    }
}