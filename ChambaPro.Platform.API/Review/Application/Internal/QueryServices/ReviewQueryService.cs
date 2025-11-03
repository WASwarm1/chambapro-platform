using ChambaPro.Platform.API.Review.Application.Internal.QueryServices;
using ChambaPro.Platform.API.Review.Domain.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Application.Internal.QueryServices
{
    public class ReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewQueryService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<Review>> Handle(GetReviewsByTechnicianIdQuery query)
        {
            var reviews = await _reviewRepository.FindByTechnicianIdAsync(query.TechnicianId);

            return reviews;
        }
    }
}