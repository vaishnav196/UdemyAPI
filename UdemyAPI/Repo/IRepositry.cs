using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Models;
using UdemyAPI.Models.Cart;
using UdemyAPI.Models.Comments;
using UdemyAPI.Models.Course;
using UdemyAPI.Models.Notification;
using UdemyAPI.Models.Promotion;
using UdemyAPI.Models.Review;
using UdemyAPI.Models.VideoProgress;

namespace UdemyAPI.Repo
{
    public interface IRepositry
    {
        bool CreateUser(User u);
        User? Login(string Email, string password);
        User GetUserByEmail(string email);

        // Block User 
        void BlockUser(string email);

        // Get All Users
        List<User> GetAllUsers();

        // --------------------------------------  ADDING COURSE  
        void CreateCategoreyCourse(AddCourseCategory cate);
        void AddContentCourse(AddCourseContent cont);
      
        List<string> GetCourseName();

        // ********* UPDATE
        List<AddCourseCategory> GetCourseAllCategories();  

       AddCourseCategory GetCourseDetailsById(int id);

        // --------------------------------------  Manage Course
        void deleteSubCourse(int id);
        void UpdateSubCourse(AddCourseContent subCourse);

        List<AddCourseContent> GetSubCourseBYCorurse(string CourseName);

        // -----------------------------------------------------------   Quiz
        
        List<AddCourseContent > GetAllContentWithCourse();

        List<string> GetAllSubcourses(string courseName);

        List<string> GetAllTitle(string GetTitles);

        //------------------------------------------------Quiz ABHANG REPO's------------------------

         Task<ActionResult<IEnumerable<string>>> GetCourses();
        Task<ActionResult<IEnumerable<string>>> GetSubcourses([FromQuery] string CourseName);
        Task<ActionResult<IEnumerable<string>>> GetTitles([FromQuery] string CourseName, [FromQuery] string SubCourseName);
        void AddQ(Quiz sc);
        List<Quiz> GetQuizByCourse(string CourseName);

        //-------------------------------------------------- Cart REPO 

        List<Cart> GetCartData(string Email);
        List<PurchaseOrder> GetPurchaseOrderData(); // Admin for tarcking // user side OrderHitory
        public void AddCart(Cart c);
        public void RemoveCart(int id);
        public void ClearCart(string Email);
        public void ProcessPayment(string email);

        List<PurchaseOrder> GetPurchasedData(string Email,string Instructor , string CourceName);

        // ---------------------------------------------- Order History 
        List<PurchaseOrder> OrderHistory(string Email);

        // -------------------------------------------- SUB COURSE DATA BY USER FROM PURCHASED TABLE
        List<AddCourseContent> GetPurchasedCourse(string Email);

        List<AddCourseContent> GetSubCourses(string Email, string CourseName);

        // ------------------------------------------------- Notification 
        void AddNotification(Notification notification);
        List<Notification> GetNotification();

        // ------------------------------------------------- Promotion
        void AddPormotions(Promotion promotion);
        List<Promotion> GetPromotions();

        // ------------------------------------------------- Video Progress
        void SaveProgress(VideoProgress progress);
        VideoProgress GetProgress(string email, string videoUrl);


        // -------------------------------------------- Comments  ----- 
         void CreateCommentAsync(Comments comment);
         List<Comments> GetApprovedCommentsAsync();

        void ApproveCommentAsync(int id);
        List<Comments> GetAllcommets();

        void DeleteCommentAsync(int id);

        List <Comments> GetCommentByCourse(string CourseName);
    
     // ---------------------------------------------- Rateing
		bool AddReview(Review review);
		double GetReviewByCourse(string CourseName);
    }
}
