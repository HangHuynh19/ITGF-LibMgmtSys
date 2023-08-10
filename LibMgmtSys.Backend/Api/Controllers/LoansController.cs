using Contracts.Loans;
using LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand;
using LibMgmtSys.Backend.Application.Loans.Queries.GetAllLoansQuery;
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
        
        public LoansController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanRequest request)
        {
            var createLoanCommand = _mapper.Map<CreateLoanCommand>(request);
            var createLoanResult = await _mediator.Send(createLoanCommand);
            
            return createLoanResult.Match(
                loans => Ok(loans.Select(result => _mapper.Map<LoanResponse>(result))),
                errors => Problem(errors));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLoans([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var getAllLoansQuery = new GetAllLoansWithPaginationQuery(pageNumber, pageSize);
            var getAllLoansResult = await _mediator.Send(getAllLoansQuery);

            return getAllLoansResult.Match(
                loans => Ok(loans.Select(result => _mapper.Map<LoanResponse>(result))),
                errors => Problem(errors));
        }
    }
}

