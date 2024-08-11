using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models.Course;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ManageCourseController : ControllerBase
	{
		private readonly IRepositry repo;
		public ManageCourseController(IRepositry repo)
		{
			this.repo = repo;
		}

		[HttpPost]
		[Route("AddCourseCategory")]
		public IActionResult AddCourseCategory(AddCourseCategory cate)
		{
			repo.CreateCategoreyCourse(cate);
			return Ok("Course Category Added Successfully");
		}

		[HttpPost]
		[Route("AddCourseContent")]
		public IActionResult AddCourseContent(AddCourseContent cont)
		{
			repo.AddContentCourse(cont);
			return Ok("Course Content Added Successfully");
		}

		[HttpGet]
		[Route("GetCourseName")]
		public IActionResult GetCourseName()
		{
			var data = repo.GetCourseName();
			return Ok(data);
		}

		[HttpGet]
		[Route("GetCourseAllCategories")]
		public IActionResult GetCourseAllCategories()
		{
			var data = repo.GetCourseAllCategories();
			return Ok(data);
		}

		[HttpGet]
		[Route("GetCourseDetailsById/{id}")]
		public IActionResult GetCourseDetailsById(int id)
		{
			var data = repo.GetCourseDetailsById(id);
			return Ok(data);
		}

		[HttpDelete]
		[Route("DeleteSubCourse/{id}")]
		public IActionResult DeleteSubCourse(int id)
		{
			repo.deleteSubCourse(id);
			return Ok("Sub Course Deleted Successfully");
		}

		[HttpPut]
		[Route("UpdateSubCourse")]
		public IActionResult UpdateSubCourse(AddCourseContent subCourse)
		{
			repo.UpdateSubCourse(subCourse);
			return Ok("Sub Course Updated Successfully");
		}

		[HttpGet]
		[Route("GetSubCourseBYCorurse/{CourseName}")]
		public IActionResult GetSubCourseBYCorurse(string CourseName)
		{
			var data = repo.GetSubCourseBYCorurse(CourseName);
			return Ok(data);
		}
	}
}
