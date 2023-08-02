using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Contracts.Authors;
using LibMgmtSys.Backend.Contracts.Books;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibMgmtSys.Backend.Api.Controlers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public AuthorsController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            Console.WriteLine(request.BookIds[0]);
            var createAuthorCommand = _mapper.Map<CreateAuthorCommand>(request);

            Console.WriteLine(createAuthorCommand.BookIds[0].Value);
            /*foreach (var bookId in createAuthorCommand.BookIds)
            {
                Console.WriteLine(bookId);
            }  */
            var createAuthorResult = await _mediator.Send(createAuthorCommand);
            var response = _mapper.Map<AuthorResponse>(createAuthorResult);
            return Ok(response);
        }
    }
}