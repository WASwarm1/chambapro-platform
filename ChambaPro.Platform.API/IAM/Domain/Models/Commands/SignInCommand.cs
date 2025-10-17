namespace ChambaPro.Platform.API.IAM.Domain.Models.Commands;

public record SignInCommand(string Email, string Password, string UserType);