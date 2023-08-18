using Contracts.Loans;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authorization;
using LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand;
using LibMgmtSys.Backend.Application.Loans.Commands.DeleteBookCommand;
using LibMgmtSys.Backend.Application.Loans.Queries.GetAllLoansQuery;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using LibMmgtSys.Backend.Api.Controllers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoansController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly IJwtTokenDecoder _jwtTokenDecoder;
        
        public LoansController(IMapper mapper, ISender mediator, IJwtTokenDecoder jwtTokenDecoder)
        {
            _mapper = mapper;
            _mediator = mediator;
            _jwtTokenDecoder = jwtTokenDecoder;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateLoan(
            [FromHeader(Name = "Authorization")] string authorization,
            [FromBody] CreateLoanRequest request)
        {
            var bearerToken = _jwtTokenDecoder.GetBearerTokenFromHeader(authorization);

            if (bearerToken is null) {
                return Unauthorized();
            }

            var userFromToken = _jwtTokenDecoder.DecodeJwtToken(bearerToken);
            var createLoanCommand = _mapper.Map<CreateLoanCommand>((request, userFromToken.UserId));
            var createLoanResult = await _mediator.Send(createLoanCommand);
            
            return createLoanResult.Match(
                loans => Ok(loans.Select(result => _mapper.Map<LoanResponse>(result))),
                errors => Problem(errors));
        }
        
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetAllLoans([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var getAllLoansQuery = new GetAllLoansWithPaginationQuery(pageNumber, pageSize);
            var getAllLoansResult = await _mediator.Send(getAllLoansQuery);

            return getAllLoansResult.Match(
                loans => Ok(loans.Select(result => _mapper.Map<LoanResponse>(result))),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan([FromRoute] string id)
        {
            var deleteLoanCommand = new DeleteLoanCommand(LoanId.Create(Guid.Parse(id)));
            var deleteLoanResult = await _mediator.Send(deleteLoanCommand);

            return deleteLoanResult.Match(
                loan => Ok(_mapper.Map<LoanResponse>(loan)),
                errors => Problem(errors));
        }
    }
}

