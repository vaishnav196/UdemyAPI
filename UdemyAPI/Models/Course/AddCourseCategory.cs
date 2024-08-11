using System.ComponentModel.DataAnnotations;

namespace UdemyAPI.Models.Course
{
    public class AddCourseCategory
    {
        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }

        public string CourseDescription { get; set; }

        public string Level { get; set; }

        public string InstructorName { get; set; }

        public string Thumbnail { get;set; }

        public double Price {  get; set; }

        public string CreatedAt { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");

        public string? Tag { get; set; }
       public int Duration { get; set; }

    }
}
