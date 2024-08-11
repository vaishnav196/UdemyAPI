using System.ComponentModel.DataAnnotations;

namespace UdemyAPI.Models.VideoProgress
{
    public class VideoProgress
    {
        [Key]
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string VideoUrl { get; set; }
        public double CurrentTime { get; set; }
    }
}
