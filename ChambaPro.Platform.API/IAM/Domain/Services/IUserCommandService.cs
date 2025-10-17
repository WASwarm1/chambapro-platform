using ChambaPro.Platform.API.IAM.Domain.Model.Aggregates;
using ChambaPro.Platform.API.IAM.Domain.Model.Commands;

namespace ChambaPro.Platform.API.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<(User?, string)> Handle(SignInCommand command);
    Task<User?> Handle(SignUpCommand command);
    Task<User?> Handle(UpdateProfileCommand command);
    Task<User?> Handle(UpdateTechnicianProfileCommand command);
}