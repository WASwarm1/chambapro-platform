using ChambaPro.Platform.API.Review.Application.Internal.CommandServices;
using ChambaPro.Platform.API.Review.Domain.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Threading.Tasks;
using System;

namespace ChambaPro.Platform.API.Review.Application.Internal.CommandServices
{
    public class ReviewCommandService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewCommandService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> Handle(SubmitReviewCommand command)
        {

            if (command.Rating < 1 || command.Rating > 5)
            {

                return false;
            }


            var newReview = new Review(
                command.TechnicianId,
                command.ClientId,
                command.Rating,
                command.Comment
            );

            await _reviewRepository.AddAsync(newReview);

            return true;
        }
    }
}