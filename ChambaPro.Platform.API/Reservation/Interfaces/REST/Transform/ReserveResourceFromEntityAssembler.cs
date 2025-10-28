﻿using ChambaPro.Platform.API.Reservation.Domain.Model.Aggregates;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;

public static class ReserveResourceFromEntityAssembler
{
    public static ReserveResource ToResourceFromEntity(Reserve entity)
    {
        return new ReserveResource(
            entity.Id,
            entity.Date,
            entity.Time.ToString(@"hh\:mm"),
            entity.Description,
            entity.ClientId,
            entity.CategoryId,
            entity.TechnicianId,
            entity.Status.ToString()
        );
    }
}