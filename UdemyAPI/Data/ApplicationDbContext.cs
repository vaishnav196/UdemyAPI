using Microsoft.EntityFrameworkCore;
using UdemyAPI.Models;
using UdemyAPI.Models.Cart;
using UdemyAPI.Models.Comments;
using UdemyAPI.Models.Course;
using UdemyAPI.Models.Notification;
using UdemyAPI.Models.Promotion;
using UdemyAPI.Models.Review;
using UdemyAPI.Models.VideoProgress;


namespace UdemyAPI.Data
{
    public class ApplicationDbContext:DbContext
    {

       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // ------------------------------------------- AUTH -----------------------
       public DbSet<User> Users { get; set; }

        // ------------------------------------------- Course Management -----------------------
        public DbSet<AddCourseCategory> addCourseCategories { get; set; }
       public DbSet<AddCourseContent> addCourseContents { get; set; }
       


        // -------------------------------------------Test QuizManagement -----------------------
        public DbSet<Quiz> Quiz { get; set; }

        // -------------------------------------------- CART Managment
        public DbSet<Cart> Cart { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
   
       // ------------------------------------------ Notification

        public DbSet<Notification> notifications { get; set; }

        // -------------------------------------------- Promotion
        public DbSet<Promotion> promotions { get; set; }
 
    
        // ---------------------------------------------- Video Progress
        public DbSet<VideoProgress> VideoProgress { get; set; }

        // ---------------------------------------------- Comments
        public DbSet<Comments> Comments { get; set; }


        // ---------------------------------------------- Rateing
        public DbSet<Review> reviews { get; set; }
    }
}
