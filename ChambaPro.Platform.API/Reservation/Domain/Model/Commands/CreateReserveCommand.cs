namespace ChambaPro.Platform.API.Reservation.Domain.Model.Commands;

public record CreateReserveCommand(
    DateTime Date,
    TimeSpan Time,
    string Description,
    string? Address,
    int ClientId,
    string CategoryId
);
