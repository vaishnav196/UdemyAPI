using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMangmentController : ControllerBase
    {
        private readonly IRepositry repo;
        public UserMangmentController(IRepositry repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("BlockUser")]
        public IActionResult BlockUser(string email)
        {
            repo.BlockUser(email);
            return Ok("User Blocked");
        }
    }
}
