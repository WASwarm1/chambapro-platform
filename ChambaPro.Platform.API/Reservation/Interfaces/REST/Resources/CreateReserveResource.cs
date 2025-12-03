namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

public record CreateReserveResource(
    DateTime Date,
    string Time,
    string Description,
    string? Address,
    int ClientId,
    string CategoryId
);
