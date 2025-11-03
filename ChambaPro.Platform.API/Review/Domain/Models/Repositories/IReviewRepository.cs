using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Domain.Models.Repositories
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
        Task<Review> FindByIdAsync(int id);
        Task<IEnumerable<Review>> FindByTechnicianIdAsync(int technicianId);
        void Remove(Review review);

    }
}