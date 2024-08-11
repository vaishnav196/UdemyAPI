using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models.Comments;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly  IRepositry repo;
        public CommentsController(IRepositry repo)
        { 
            this.repo = repo;
        }

        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment(Comments comment)
        {
            repo.CreateCommentAsync(comment);
            return Ok("Comment Added sucessfully");
        }


        [HttpGet]
        [Route("GetApproveComments")]  // for user
        public  IActionResult GetApprovedComments()
        {
            var comments = repo.GetApprovedCommentsAsync();
            return Ok(comments);
        }
        [HttpPost]
        [Route("ApproveComments/{id}")]
        public IActionResult ApproveComments(int id)
        {
            repo.ApproveCommentAsync(id);
            return Ok("Comment Approved");
        }
        [HttpGet]
        [Route("GetAllUnapporveComments")]
        public IActionResult GetAllUnapporveComments() {
            var comments = repo.GetAllcommets();
            return Ok(comments);
        }

        [HttpDelete]
		[Route("DeleteComments/{id}")]
		public IActionResult DeleteComment(int id) {
			repo.DeleteCommentAsync(id);
			return Ok("Comment Deleted");
		}

        [HttpGet]
        [Route("GetCommentsByCourseName")]
        public IActionResult GetCommentsByCourseName(string courseName)
		{
			var comments = repo.GetCommentByCourse(courseName);
			return Ok(comments);
		}
               
    }
}
