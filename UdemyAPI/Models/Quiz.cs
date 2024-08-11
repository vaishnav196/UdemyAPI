using System.ComponentModel.DataAnnotations;

namespace UdemyAPI.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Subcourse { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
    }
}
