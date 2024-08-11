using System.ComponentModel.DataAnnotations;

namespace UdemyAPI.Models.Promotion
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        public string CourseImage { get; set; }
        public string CourseName { get; set; }

        public string Description { get; set; }
        public double OfferPrice { get; set; }
        public string ValidUpTo { get; set; }
    }
}
