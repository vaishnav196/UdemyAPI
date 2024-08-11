using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models.Notification;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class NotificationController : ControllerBase
    {

        private readonly IRepositry repo;

        public NotificationController(IRepositry repo)
        {
            this.repo = repo;
        }


        [HttpPost]
        [Route("PostNotification")]
        public IActionResult PostNotification(Notification notification)
        {
            repo.AddNotification(notification);
            return Ok("Added sucessfully");
        }

        [HttpGet]
        [Route("GetNotification")]
        public IActionResult GetAllNotification()
        {
            var data = repo.GetNotification();
            return Ok(data);
        }
    }
}
