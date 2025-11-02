using ChambaPro.Platform.API.Review.Domain.Models.Commands;
using ChambaPro.Platform.API.Review.Domain.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Application.Internal.CommandServices
{
    // Este servicio recibe el comando y contiene la lógica de negocio
    public class ReviewCommandService
    {
        private readonly IReviewRepository _reviewRepository;

        // Inyección de dependencias: se le pasa el repositorio al crear el servicio
        public ReviewCommandService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // Método para procesar el comando de enviar reseña
        public async Task<bool> Handle(SubmitReviewCommand command)
        {
            // 1. Lógica de Dominio (crear la entidad)
            var newReview = new Review(
                command.TechnicianId,
                command.ClientId,
                command.Rating,
                command.Comment
            );

            // 2. Persistencia (guardar en la base de datos)
            await _reviewRepository.AddAsync(newReview);

            // En un sistema real, aquí iría lógica adicional (ej. notificar al técnico)
            return true; // Éxito
        }
    }
}