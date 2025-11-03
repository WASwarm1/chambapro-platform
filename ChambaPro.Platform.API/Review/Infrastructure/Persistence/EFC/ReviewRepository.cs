using ChambaPro.Platform.API.Review.Domain.Models.Repositories;
using ChambaPro.Platform.API.Review.Domain.Models.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChambaPro.Platform.API.Review.Infrastructure.Persistence.EFC
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DbContext _context;

        public ReviewRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Review review)
        {
            await _context.Set<Review>().AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public void Remove(Review review)
        {
            _context.Set<Review>().Remove(review);
            _context.SaveChanges();
        }

        public async Task<Review> FindByIdAsync(int id)
        {
            return await _context.Set<Review>().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> FindByTechnicianIdAsync(int technicianId)
        {
            return await _context.Set<Review>()
                .Where(r => r.TechnicianId == technicianId)
                .ToListAsync();
        }
    }
}