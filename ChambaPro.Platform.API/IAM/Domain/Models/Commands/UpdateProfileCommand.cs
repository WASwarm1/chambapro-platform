namespace ChambaPro.Platform.API.IAM.Domain.Models.Commands;

public record UpdateProfileCommand(
    int UserId,
    string Name,
    string Lastname,
    string Phone,
    string? Avatar = null
);