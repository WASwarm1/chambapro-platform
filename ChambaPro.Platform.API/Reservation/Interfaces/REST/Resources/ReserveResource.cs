namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

public record ReserveResource(
    int Id,
    DateTime Date,
    string Time,
    string Description,
    string ClientId,
    string CategoryId,
    string? TechnicianId,
    string Status
);