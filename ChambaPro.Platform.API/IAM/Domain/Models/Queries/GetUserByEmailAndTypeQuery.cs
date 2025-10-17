namespace ChambaPro.Platform.API.IAM.Domain.Models.Queries;

public record GetUserByEmailAndTypeQuery(string Email, string UserType);