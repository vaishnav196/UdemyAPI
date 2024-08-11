using System.ComponentModel.DataAnnotations;

namespace UdemyAPI.Models.Notification
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
