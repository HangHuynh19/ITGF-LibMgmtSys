using Contracts.Genres;
using LibMgmtSys.Backend.Application.Genres.Commands.CreateGenreCommand;
using LibMgmtSys.Backend.Application.Genres.Queries;
using LibMmgtSys.Backend.Api.Controllers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllGenres()
        {
            var getAllGenresQuery = new GetAllGenresQuery();
            var getAllGenresResult = await _mediator.Send(getAllGenresQuery);

            return getAllGenresResult.Match(
                genres => Ok(_mapper.Map<List<GenreResponse>>(genres)),
                errors => Problem(errors));
        }
        
        [HttpPost]
        [Authorize(Policy = "Admin")]
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