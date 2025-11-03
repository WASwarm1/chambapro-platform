using ChambaPro.Platform.API.Review.Application.Commands;
using ChambaPro.Platform.API.Review.Application.Queries;
using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Review.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Assemblers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Mime;

namespace ChambaPro.Platform.API.Review.Interfaces.Rest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewCommandService _commandService;
        private readonly ReviewQueryService _queryService;
        private readonly ReviewResourceAssembler _assembler;

        public ReviewController(ReviewCommandService commandService, ReviewQueryService queryService, ReviewResourceAssembler assembler)
        {
            _commandService = commandService;
            _queryService = queryService;
            _assembler = assembler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewResource), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public async Task<IActionResult> SubmitReview([FromBody] CreateReviewResource resource)
        {
            var command = _assembler.ToCommandFromResource(resource);

            var success = await _commandService.Handle(command);

            if (!success)
            {
                return BadRequest("No se pudo procesar la reseña, verifique los datos.");
            }

            return StatusCode(201, "Reseña creada exitosamente.");
        }

        [HttpGet("technician/{technicianId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewResource>), 200)]
        public async Task<IActionResult> GetReviewsByTechnicianId(int technicianId)
        {
            var query = new GetReviewsByTechnicianIdQuery(technicianId);

            var reviews = await _queryService.Handle(query);

            var reviewResources = _assembler.ToResourceListFromEntityList(reviews);

            return Ok(reviewResources);
        }
    }
}