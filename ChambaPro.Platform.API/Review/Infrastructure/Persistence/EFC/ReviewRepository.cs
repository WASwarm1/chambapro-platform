using ChambaPro.Platform.API.Review.Domain.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChambaPro.Platform.API.Review.Infrastructure.Persistence.EFC
{

    public class ReviewRepository : IReviewRepository
    {
        // Necesitas un contexto de Entity Framework para acceder a la base de datos




        public Task AddAsync(Review review)
        {

            throw new NotImplementedException();
        }

        public Task<Review> FindByIdAsync(int id)
        {

            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> FindByTechnicianIdAsync(int technicianId)
        {

            throw new NotImplementedException();
        }

        public void Remove(Review review)
        {

            throw new NotImplementedException();
        }
    }
}