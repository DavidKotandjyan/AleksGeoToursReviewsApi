using Microsoft.EntityFrameworkCore;
using AleksGeoToursReviewsApi.Models;

namespace AleksGeoToursReviewsApi.Data
{
    public class ReviewsContext : DbContext
    {
        public ReviewsContext(DbContextOptions<ReviewsContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }
    }
}
