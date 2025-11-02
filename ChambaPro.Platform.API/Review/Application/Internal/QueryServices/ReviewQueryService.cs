using ChambaPro.Platform.API.Review.Domain.Models.Queries;
using ChambaPro.Platform.API.Review.Domain.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Application.Internal.QueryServices
{
    // Este servicio recibe la consulta y obtiene los datos del repositorio
    public class ReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;

        // Inyección de dependencias
        public ReviewQueryService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // Método para procesar la consulta de obtener reseñas por técnico
        public async Task<IEnumerable<Review>> Handle(GetReviewsByTechnicianIdQuery query)
        {
            // 1. Obtener datos del repositorio
            var reviews = await _reviewRepository.FindByTechnicianIdAsync(query.TechnicianId);

            // 2. Devolver la lista de reseñas
            return reviews;
        }
    }
}