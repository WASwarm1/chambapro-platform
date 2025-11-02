namespace ChambaPro.Platform.API.Review.Domain.Models.Commands
{
    // Este objeto transporta los datos que vienen del formulario del cliente
    public class SubmitReviewCommand
    {
        public int TechnicianId { get; set; } // ¿A quién se le da la reseña?
        public int ClientId { get; set; }     // ¿Quién está dando la reseña?
        public int Rating { get; set; }       // Puntuación (1-5)
        public string Comment { get; set; }   // Comentario del cliente
    }
}