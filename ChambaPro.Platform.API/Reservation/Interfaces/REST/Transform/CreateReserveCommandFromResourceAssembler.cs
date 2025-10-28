using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;

public static class CreateReserveCommandFromResourceAssembler
{
    public static CreateReserveCommand ToCommandFromResource(CreateReserveResource resource)
    {
        var time = TimeSpan.Parse(resource.Time);
        
        return new CreateReserveCommand(
            resource.Date,
            time,
            resource.Description,
            resource.ClientId,
            resource.CategoryId
        );
    }
}