using ErrorOr;
using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Contracts.Authors;
using LibMgmtSys.Backend.Contracts.Books;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using LibMmgtSys.Backend.Api.Controllers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibMgmtSys.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorsController : ApiController
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
            
            var createAuthorCommand = _mapper.Map<CreateAuthorCommand>(request);
            var createAuthorResult = await _mediator.Send(createAuthorCommand);
            
            return createAuthorResult.Match(
                author => Ok(_mapper.Map<AuthorResponse>(author)),
                errors => Problem(errors));
        }
        
        
    }
}
