using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyAPI.Data;
using UdemyAPI.Models;
using UdemyAPI.Models.Course;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly IRepositry repo;
        private readonly ApplicationDbContext db;
        public CourseController(IRepositry repo, ApplicationDbContext db)
        {
            this.repo = repo;
            this.db = db;
        }
        

       

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddnewCategorey( AddCourseCategory cate)
        {
            repo.CreateCategoreyCourse(cate);
            return Ok("Categorey Added sucessfully");
        }

        [HttpPost]
        [Route("AddContent")]
        public IActionResult AddContent( AddCourseContent cont)
        {
            repo.AddContentCourse(cont);
            return Ok("Content Added sucessfully");
        }

        [HttpGet]
        [Route("GetCourseName")]
        public IActionResult GetCourse()
        {
            var data = repo.GetCourseName();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetAllCourse")]
        public IActionResult GetAllCourse()
        {
             var data = repo.GetCourseAllCategories();
             return Ok(data);
        }

        [HttpGet]
        [Route("GetCourseById/{id}")]
        public IActionResult GetCourseByID(int id)
        {
            var data = repo.GetCourseDetailsById(id);
            return Ok(data);
        }



        // --------------------------------------------    MANAGEMENT
      

        [HttpGet]
        [Route("GetALLCourseWithSubcourse")]
        public IActionResult GetCoueSub()
        {
            var data = repo.GetAllContentWithCourse();
            return Ok(data);
        }


        [HttpGet]
        [Route("GetSubCourse")]
        public IActionResult GetSubcourses(string courseName)
        {
            var subcourses = repo.GetAllSubcourses(courseName);
            return Ok(subcourses);
        }

        [HttpGet]
        [Route("GetTitles")]
        public IActionResult GetTitles(string courseName)
        {
            var Titles = repo.GetAllTitle(courseName);
            return Ok(Titles);
        }


        //---------------------------------------------ABHANG QUIZ API's With Repo--------------------------------------------------
        //[HttpGet]
        //[Route("GetCourseNameABH")]
        //public IActionResult GetCourseABH()
        //{
        //    var CourseName = repo.GetCourses();
        //    return Ok(CourseName);
        //}

        //[HttpGet]
        //[Route("GetSubCourseABH")]
        //public IActionResult GetSUBCourseABH(string CourseName)
        //{
        //    var CourseNameABH = repo.GetSubcourses(CourseName);
        //    return Ok(CourseNameABH);
        //}

        //[HttpGet]
        //[Route("GetTitleABH")]
        //public IActionResult GetTitleABH(string CourseName, string SubCourseName) 
        //{
        //    var TitleAbh = repo.GetTitles(CourseName, SubCourseName);
        //    return Ok(TitleAbh);
        //}

        //[HttpPost]
        //[Route("AddQuizABH")]
        //public IActionResult AddQuizABH(Quiz z)
        //{
        //    repo.AddQ(z);
        //    return Ok("Quiz Added sucessfully");

        //}

        //---------------------------------------------ABHANG QUIZ API's WithOut Repo--------------------------------------------------



        [HttpGet("GetQuizByCourse")]
        public IActionResult GetQuizByCourse(string CourseName)
        {
            var quizzes = repo.GetQuizByCourse(CourseName);
            return Ok(quizzes);
        }

        [HttpGet]
        [Route("GetCourseNameABH")]
        public async Task<ActionResult<IEnumerable<string>>> GetCourses()
        
        {
            var CourseName = await db.addCourseContents.Select(c => c.CourseName).Distinct().ToListAsync();
            return Ok(CourseName);
        }

        [HttpGet]
        [Route("GetSubCourseABH")]
        public async Task<ActionResult<IEnumerable<string>>> GetSubcoursesABH([FromQuery] string CourseName)
        {
            var SubCourseName = await db.addCourseContents.Where(c => c.CourseName == CourseName)
                                                    .Select(c => c.SubCourse)
                                                    .Distinct()
                                                    .ToListAsync();
            return Ok(SubCourseName);
        }

        [HttpGet]
        [Route("GetTitleABH")]
        public async Task<ActionResult<IEnumerable<string>>> GetTitles([FromQuery] string CourseName, [FromQuery] string SubCourseName)
        {
            var Title = await db.addCourseContents.Where(c => c.CourseName == CourseName && c.SubCourse == SubCourseName)
                                                      .Select(c => c.Title)
                                                      .Distinct()
                                                      .ToListAsync();
            return Ok(Title);
        }

        [HttpPost]
        [Route("AddQuizABH")]
        public IActionResult AddQ(Quiz sc)
        {
            db.Quiz.Add(sc);
            db.SaveChanges();
            return Ok("Deatil Added sucessfully");
        }
    }
}
