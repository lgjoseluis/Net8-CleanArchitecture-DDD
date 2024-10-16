
using Alfa.CarRental.Application.Abstractions.Clock;
using Alfa.CarRental.Application.Abstractions.Messaging;
using Alfa.CarRental.Domain.Abstractions;
using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Users;
using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Application.Rentals.BookRental;

internal sealed class BookRentalCommandHandler : ICommandHandler<BookRentalCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentalRepository _rentalRepository;
    private readonly PriceService _priceService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BookRentalCommandHandler(
        IUserRepository userRepository, 
        IVehicleRepository vehicleRepository, 
        IRentalRepository rentalRepository, 
        PriceService priceService, 
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _rentalRepository = rentalRepository;
        _priceService = priceService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(BookRentalCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.FindByIdAsync(request.UserId, cancellationToken);
        Vehicle? vehicle = await _vehicleRepository.FindByIdAsync(request.VehicleId, cancellationToken);

        if (user is null) 
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        if (vehicle is null)
        {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        DateRange dateRange = DateRange.Create(request.StartDate, request.EndDate);

        if (await _rentalRepository.IsOverlappingAsync(vehicle, dateRange, cancellationToken))
        {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }

        Rental rental = Rental.Reserve(vehicle, user.Id, dateRange, _dateTimeProvider.CurrentTime, _priceService);

        _rentalRepository.Add(rental);

        await _unitOfWork.SaveChangesAsync(cancellationToken);      
        
        return rental.Id;
    }
}