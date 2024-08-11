using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models.Review;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IRepositry repo;
        public ReviewController(IRepositry repo)
		{
			this.repo = repo;
		}
		[HttpPost]
		[Route("AddReview")]
		public IActionResult AddReview(Review r)
		{
			bool reviewAdded = repo.AddReview(r);
			if (!reviewAdded)
			{
				return BadRequest("Review not added.");
			}
			return Ok("Review added successfully.");
		}

		[HttpGet]
		[Route("GetReviewAvg")]
		public IActionResult GetReviewAvg(string courseName)
		{
			double averageRating = repo.GetReviewByCourse(courseName);

			if (double.IsNaN(averageRating))
			{
				return BadRequest("No reviews found.");
			}

			return Ok(averageRating);
		}
	}



}
