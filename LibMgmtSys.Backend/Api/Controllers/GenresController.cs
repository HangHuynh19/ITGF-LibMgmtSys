using Contracts.Genres;
using LibMgmtSys.Backend.Application.Genres;
using LibMmgtSys.Backend.Api.Controllers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenresController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        
        public GenresController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
        {
            var createGenreCommand = _mapper.Map<CreateGenreCommand>(request);
            var createGenreResult = await _mediator.Send(createGenreCommand);
            
            return createGenreResult.Match(
                genre => Ok(_mapper.Map<GenreResponse>(genre)),
                errors => Problem(errors));
        }
    }
}

