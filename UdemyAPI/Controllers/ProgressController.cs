using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models.VideoProgress;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IRepositry repo;
        public ProgressController(IRepositry repo)
        {
            this.repo = repo;
        }

   
        [HttpPost("SaveVideoProgress")]
        public IActionResult SaveVideoProgress(VideoProgress progress)
        {
            if (progress == null)
            {
                return BadRequest("Invalid data.");
            }
 
             
            repo.SaveProgress(progress);

            return Ok("add succes");
        }


        [HttpGet("GetVideoProgress")]
        public IActionResult GetVideoProgress( string Email, string url)
        {
        

            var progress = repo.GetProgress(Email,url);
            if (progress == null)
            {
                return NotFound("No progress found.");
            }

            return Ok(progress);
        }
    }
}
