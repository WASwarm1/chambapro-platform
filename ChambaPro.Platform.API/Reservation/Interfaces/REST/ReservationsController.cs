﻿using System.Net.Mime;
using ChambaPro.Platform.API.Reservation.Domain.Model.Commands;
using ChambaPro.Platform.API.Reservation.Domain.Model.Queries;
using ChambaPro.Platform.API.Reservation.Domain.Services;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Resources;
using ChambaPro.Platform.API.Reservation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ChambaPro.Platform.API.Reservation.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Operations for managing reservations")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Reservations")]
public class ReservationsController(
    IReserveCommandService reserveCommandService, 
    IReserveQueryService reserveQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new Reservation",
        Description = "Creates a new Reservation",
        OperationId = "CreateReservation")]
    [SwaggerResponse(StatusCodes.Status201Created, "The Reservation Was Created", typeof(ReserveResource))]
    public async Task<ActionResult> CreateReservation([FromBody] CreateReserveResource resource)
    {
        var createReserveCommand = CreateReserveCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await reserveCommandService.Handle(createReserveCommand);
        
        if (result == null)
            return BadRequest();
        
        return CreatedAtAction(
            nameof(GetReservationById), 
            new { id = result.Id }, 
            ReserveResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Gets a Reservation by Id",
        Description = "Gets a Reservation by Id",
        OperationId = "GetReservationById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservation Was Found", typeof(ReserveResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> GetReservationById(int id)
    {
        var getReserveByIdQuery = new GetReserveByIdQuery(id);
        var result = await reserveQueryService.Handle(getReserveByIdQuery);
        
        if (result == null)
            return NotFound();
        
        var reserveResource = ReserveResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(reserveResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all Reservations or filters by clientId/technicianId",
        Description = "Gets all Reservations or filters by clientId/technicianId",
        OperationId = "GetAllReservations")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservations Were Found", typeof(IEnumerable<ReserveResource>))]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] string? clientId, 
        [FromQuery] string? technicianId)
    {
        if (!string.IsNullOrEmpty(clientId))
        {
            var getReservesByClientIdQuery = new GetReservesByClientIdQuery(clientId);
            var clientReserves = await reserveQueryService.Handle(getReservesByClientIdQuery);
            var clientResult = clientReserves.Select(ReserveResourceFromEntityAssembler.ToResourceFromEntity).ToList();
            return Ok(clientResult);
        }

        if (!string.IsNullOrEmpty(technicianId))
        {
            var getReservesByTechnicianIdQuery = new GetReservesByTechnicianIdQuery(technicianId);
            var technicianReserves = await reserveQueryService.Handle(getReservesByTechnicianIdQuery);
            var technicianResult = technicianReserves.Select(ReserveResourceFromEntityAssembler.ToResourceFromEntity).ToList();
            return Ok(technicianResult);
        }

        var getAllReservesQuery = new GetAllReservesQuery();
        var reserves = await reserveQueryService.Handle(getAllReservesQuery);
        var result = reserves.Select(ReserveResourceFromEntityAssembler.ToResourceFromEntity).ToList();
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Updates a Reservation",
        Description = "Updates a Reservation",
        OperationId = "UpdateReservation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservation Was Updated", typeof(ReserveResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> UpdateReservation(int id, [FromBody] UpdateReserveResource resource)
    {
        if (id != resource.Id)
        {
            return BadRequest("Invalid Reservation Id");
        }
        
        var updateReserveCommand = UpdateReserveCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await reserveCommandService.Handle(updateReserveCommand);

        if (result == null)
        {
            return NotFound("The Reservation Was Not Found");
        }
        
        return Ok(ReserveResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpPatch("{id:int}/cancel")]
    [SwaggerOperation(
        Summary = "Cancels a Reservation by Id",
        Description = "Cancels a Reservation with the given Id",
        OperationId = "CancelReservation")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Reservation Was Cancelled", typeof(ReserveResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> CancelReservation(int id)
    {
        var cancelReserveCommand = new CancelReserveCommand(id);
        var result = await reserveCommandService.Handle(cancelReserveCommand);

        if (result == null)
        {
            return NotFound("The Reservation Was Not Found");
        }

        return Ok(ReserveResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Deletes a Reservation by Id",
        Description = "Deletes a Reservation with the given Id",
        OperationId = "DeleteReservation")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Reservation Was Deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Reservation Not Found")]
    public async Task<ActionResult> DeleteReservation(int id)
    {
        var deleteReserveCommand = new DeleteReserveCommand(id);
        var result = await reserveCommandService.Handle(deleteReserveCommand);

        if (result == null)
        {
            return NotFound("The Reservation Was Not Found");
        }
        
        return NoContent();
    }
}