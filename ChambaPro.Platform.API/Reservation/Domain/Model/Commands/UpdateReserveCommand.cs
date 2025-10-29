using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;

namespace ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

public record UpdateReserveCommand(
    int ReserveId,
    DateTime Date,
    TimeSpan Time,
    string Description,
    string ClientId,
    string CategoryId,
    string? TechnicianId,
    ReservationStatus Status
);