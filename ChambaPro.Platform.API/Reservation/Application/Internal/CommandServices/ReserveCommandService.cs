using Chambapro_backend.Shared.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Domain.Repositories;
using ChambaPro.Platform.API.Reservation.Domain.Services;

namespace ChambaPro.Platform.API.Reservation.Application.Internal.CommandServices;

public class ReserveCommandService(IReserveRepository repository, IUnitOfWork unitOfWork) 
    : IReserveCommandService
{
    public async Task<Reserve?> Handle(CreateReserveCommand command)
    {
        var reserve = new Reserve(command);
        await repository.AddAsync(reserve);
        await unitOfWork.CompleteAsync();
        return reserve;
    }

    public async Task<Reserve?> Handle(UpdateReserveCommand command)
    {
        var reserve = await repository.FindByIdAsync(command.ReserveId);
        
        if (reserve == null)
            return null;

        reserve.UpdateFromCommand(command);

        try
        {
            repository.Update(reserve);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update reserve {command.ReserveId}", e);
        }

        return reserve;
    }

    public async Task<Reserve?> Handle(DeleteReserveCommand command)
    {
        var reserve = await repository.FindByIdAsync(command.Id);
        
        if (reserve == null)
            return null;

        try
        {
            repository.Remove(reserve);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete reserve {command.Id}", e);
        }
        
        return reserve;
    }

    public async Task<Reserve?> Handle(CancelReserveCommand command)
    {
        var reserve = await repository.FindByIdAsync(command.ReserveId);
        
        if (reserve == null)
            return null;

        reserve.Cancel();

        try
        {
            repository.Update(reserve);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to cancel reserve {command.ReserveId}", e);
        }

        return reserve;
    }
}