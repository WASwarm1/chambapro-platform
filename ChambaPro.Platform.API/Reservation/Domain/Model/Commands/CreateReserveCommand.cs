namespace ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

public record CreateReserveCommand(
    DateTime Date,
    TimeSpan Time,
    string Description,
    string ClientId,
    string CategoryId
);