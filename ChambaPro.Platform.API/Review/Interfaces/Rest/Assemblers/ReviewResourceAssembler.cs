using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using ChambaPro.Platform.API.Review.Interfaces.Rest.Resources;
using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using System.Collections.Generic;
using System.Linq;

namespace ChambaPro.Platform.API.Review.Interfaces.Rest.Assemblers
{
    public class ReviewResourceAssembler
    {
        public SubmitReviewCommand ToCommandFromResource(CreateReviewResource resource)
        {
            return new SubmitReviewCommand
            {
                TechnicianId = resource.TechnicianId,
                ClientId = resource.ClientId,
                Rating = resource.Rating,
                Comment = resource.Comment
            };
        } // <-- Esta llave faltaba

        public ReviewResource ToResourceFromEntity(Review entity)
        {
            return new ReviewResource
            {
                Id = entity.Id,
                Rating = entity.Rating,
                Comment = entity.Comment,
                CreationDate = entity.CreationDate,
                ClientName = "Nombre de Cliente Placeholder"
            };
        }

        public IEnumerable<ReviewResource> ToResourceListFromEntityList(IEnumerable<Review> entityList)
        {
            return entityList.Select(ToResourceFromEntity);
        }
    }
}