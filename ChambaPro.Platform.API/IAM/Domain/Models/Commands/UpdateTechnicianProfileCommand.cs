namespace ChambaPro.Platform.API.IAM.Domain.Models.Commands;

public record UpdateTechnicianProfileCommand(
    int UserId,
    string Speciality,
    string Description,
    string Experience,
    decimal HourlyRate,
    bool IsAvailable
);