using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyAPI.Models.Course
{
    public class AddCourseContent
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaUrl { get; set;}
        public string SubCourse { get; set; }
        public string CourseName { get; set; } // Drop Down from Admin Course Table 
        public string Question { get; set; }
      
    }
}
