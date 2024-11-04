using Microsoft.AspNetCore.Mvc;

using MediatR;

using Alfa.CarRental.Application.Rentals.GetRentalQuery;
using Alfa.CarRental.Application.Rentals.BookRental;
using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.WebApi.Controllers.Rentals
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ISender _sender;

        public RentalController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRental(Guid id, CancellationToken cancellationToken)
        {
            GetRentalQuery query = new GetRentalQuery(id);

            Result<RentalResponse> result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> BookRental(RentalRequest request, CancellationToken cancellationToken) 
        {
            BookRentalCommand command =  new BookRentalCommand(request.VehicleId, request.UserId, request.StartDate, request.EndDate);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            { 
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetRental), new { id = result.Value}, result.Value);
        }
    }
}
