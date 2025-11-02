using ChambaPro.Platform.API.Review.Domain.Model.Commands;
using ChambaPro.Platform.API.Review.Domain.Model.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReviewController : ControllerBase
    {
        // Necesitas inyectar aquí los servicios de Command y Query para usarlos.



        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] SubmitReviewCommand command)
        {

            return StatusCode(201); // Created
        }

        [HttpGet("{technicianId:int}")]
        public async Task<IActionResult> GetReviewsByTechnicianId(int technicianId)
        {

            return Ok(null); // Devuelve la lista de reseñas
        }
    }
}