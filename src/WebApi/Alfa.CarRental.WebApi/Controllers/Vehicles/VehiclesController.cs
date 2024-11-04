using Microsoft.AspNetCore.Mvc;

using MediatR;

using Alfa.CarRental.Application.Vehicles.SearchVehicles;
using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.WebApi.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ISender _sender;

        public VehiclesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchVehicles(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
        {
            SearchVehiclesQuery query = new SearchVehiclesQuery(startDate, endDate);

            Result<IReadOnlyList<VehicleResponse>> result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);

        }
    }
}
