using AleksGeoToursReviewsApi.Data;
using AleksGeoToursReviewsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AleksGeoToursReviewsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewsContext _context;

        public ReviewsController(ReviewsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reviews = await _context.Reviews.OrderByDescending(r => r.CreatedAt).ToListAsync();
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Review review)
        {
            if (string.IsNullOrWhiteSpace(review.Name) || string.IsNullOrWhiteSpace(review.Message))
                return BadRequest("Name and Message are required.");

            review.CreatedAt = DateTime.Now;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = review.Id }, review);
        }
        
    }
}
