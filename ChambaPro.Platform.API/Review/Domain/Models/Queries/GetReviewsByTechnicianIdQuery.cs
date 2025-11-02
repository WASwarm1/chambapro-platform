namespace ChambaPro.Platform.API.Review.Domain.Models.Queries
{
    // Este objeto solo necesita el ID del técnico para buscar sus reseñas
    public class GetReviewsByTechnicianIdQuery
    {
        public int TechnicianId { get; set; }

        public GetReviewsByTechnicianIdQuery(int technicianId)
        {
            TechnicianId = technicianId;
        }
    }
}