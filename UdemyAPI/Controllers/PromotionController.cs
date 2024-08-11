using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models.Promotion;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IRepositry repo;

        public PromotionController(IRepositry repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("PostPromotion")]
        public IActionResult PostPromotion(Promotion promotion)
        {
            repo.AddPormotions(promotion);
            return Ok("Added sucessfully");
        }

        [HttpGet]
        [Route("GetPromotion")]
        public IActionResult GetPromotion() {
            var data = repo.GetPromotions();
            return Ok(data);
        }

    }
}
