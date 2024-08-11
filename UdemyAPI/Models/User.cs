using System.ComponentModel.DataAnnotations;

namespace UdemyAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";
        public string Contact { get; set; }
        public string? RegisteredDate { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
    }
}
