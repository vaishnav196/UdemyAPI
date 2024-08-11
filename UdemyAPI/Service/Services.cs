using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyAPI.Data;
using UdemyAPI.Models;
using UdemyAPI.Models.Cart;
using UdemyAPI.Models.Comments;
using UdemyAPI.Models.Course;
using UdemyAPI.Models.Notification;
using UdemyAPI.Models.Promotion;
using UdemyAPI.Models.Review;
using UdemyAPI.Models.VideoProgress;
using UdemyAPI.Repo;

namespace UdemyAPI.Service
{
    public class Services : IRepositry
    {
        private readonly ApplicationDbContext db;

     
        public Services(ApplicationDbContext db) 
        { 
         this.db = db;
        }

        // -----------------------   USER AUTHENTICATION  ----------------------
        public bool  CreateUser(User u)
        {
            // Check if the email already exists in the database
            if (db.Users.Any(user => user.Email == u.Email))
            {
                return false; // Email already exists
            }

            // Add the new user to the database
            db.Users.Add(u);
            db.SaveChanges();

            return true; // User created successfully
        }
        public User? Login(string Email, string password)
        {
            try
            {
                // Find user by email and password
                var user = db.Users.FirstOrDefault(u => u.Email == Email && u.Password == password);

                return user;

            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                throw new Exception("Error during login", ex);
            }
        }


        public User GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }



        // --------------------------------  COURSE CONTENT

        public void CreateCategoreyCourse(AddCourseCategory cate)
        {
            db.addCourseCategories.Add(cate);
            db.SaveChanges();
        }

        public void AddContentCourse(AddCourseContent cont)
        {
          db.addCourseContents.Add(cont);
          db.SaveChanges();
        }

        public List<string> GetCourseName()
        {
            var data = db.addCourseCategories
                          .Select(c => c.CourseName)
                          .ToList();
            return data;
        }

        public List<AddCourseCategory> GetCourseAllCategories()
        {
            var categories = db.addCourseCategories.ToList();
            return categories;
        }
        public AddCourseCategory GetCourseDetailsById(int id)
        {
            var category = db.addCourseCategories.FirstOrDefault(c => c.Id == id);
            return category;
        }
        // ----------------------------------------- QUIZ 
  

        public List<AddCourseContent> GetAllContentWithCourse()
        {
             var data = db.addCourseContents.ToList();
             return data;
        }

        public List<string> GetAllSubcourses(string courseName)
        {
            var subcourses = db.addCourseContents
            .Where(c => c.CourseName == courseName)
            .Select(c => c.SubCourse)
            .ToList();
            return subcourses;
        }

        public List<string> GetAllTitle(string courseName)
        {
            var Titles = db.addCourseContents
            .Where(c => c.CourseName == courseName)
            .Select(c => c.Title)
            .ToList();
            return Titles;
        }


        //-----------------------------------------------ABHANG QUIZ service-----------------------------------


        public List<Quiz> GetQuizByCourse(string CourseName)
        {
            var quizzes = db.Quiz.Where(q => q.CourseName == CourseName).ToList();
            return quizzes;
        }

        public async Task<ActionResult<IEnumerable<string>>> GetCourses()
        {
            var CourseName = await db.addCourseContents.Select(c => c.CourseName).Distinct().ToListAsync();
            return CourseName;
        }

        public async Task<ActionResult<IEnumerable<string>>> GetSubcourses([FromQuery] string CourseName)
        {
            var SubCourseName = await db.addCourseContents.Where(c => c.CourseName == CourseName)
                                                     .Select(c => c.SubCourse)
                                                     .Distinct()
                                                     .ToListAsync();
            return SubCourseName;
        }

        public async Task<ActionResult<IEnumerable<string>>> GetTitles([FromQuery] string CourseName, [FromQuery] string SubCourseName)
        {
            var Title = await db.addCourseContents.Where(c => c.CourseName == CourseName && c.SubCourse == SubCourseName)
                                                     .Select(c => c.Title)
                                                     .Distinct()
                                                     .ToListAsync();
            return Title;
        }

        public void AddQ(Quiz sc)
        {
            db.Quiz.Add(sc);
            db.SaveChanges();
        }



        // --------------------------------------------------- CART service

        public void AddCart(Cart c)
        {
            db.Cart.Add(c);
            db.SaveChanges();
        }

        public void RemoveCart(int id)
        {
          var  data=db.Cart.Find(id);
          db.Cart.Remove(data);
          db.SaveChanges();
        }

        public void ClearCart(string Email)
        {
            var cartItems = db.Cart.Where(c => c.Email == Email).ToList();
            db.Cart.RemoveRange(cartItems);
            db.SaveChanges();
        }

        public List<Cart> GetCartData(string Email)
        {
            var cartItems = db.Cart.Where(c => c.Email == Email).ToList();
            return cartItems;
        }

        public void ProcessPayment(string email)
        {
            // Retrieve cart items for the user
            var cartItems = db.Cart.Where(c => c.Email == email).ToList();

            // Convert cart items to purchase order entries
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
            foreach (var item in cartItems)
            {
                // Create a purchase order and set the properties
                var purchaseDate = DateTime.Now;
                var endDate = purchaseDate.AddDays(item.Duration);

                PurchaseOrder order = new PurchaseOrder
                {
                    CourseName = item.CourseName,
                    InstructorName = item.InstructorName,
                    Email = item.Email,
                    Price = item.Price,
                    Duration = item.Duration,
                    PurchasedDate = purchaseDate.ToString("dd-MM-yyyy"),
                    EndDate = endDate.ToString("dd-MM-yyyy")
                };

                purchaseOrders.Add(order);
            }

            // Save purchase orders to the database
            db.PurchaseOrder.AddRange(purchaseOrders);

            // Clear the cart
            db.Cart.RemoveRange(cartItems);

            // Save changes to the database
            db.SaveChanges();
        }

        public List<PurchaseOrder> GetPurchaseOrderData()
        {
            var data =db.PurchaseOrder.ToList();
            return data;
        }

        public List<PurchaseOrder> GetPurchasedData(string email, string instructor, string courseName)
        {
            var purchasedData = db.PurchaseOrder
                .Where(po => po.Email == email && po.InstructorName == instructor && po.CourseName == courseName)
                .ToList();

            return purchasedData;
        }

      
        // ------------------------------------------------ Order Hitsory
        public List<PurchaseOrder> OrderHistory(string Email)
        {
            var data = db.PurchaseOrder.Where(e=>e.Email== Email).ToList();
            return data;
        }



        // --------------------------------------------------- PURCHASED COURSE

        public List<AddCourseContent> GetPurchasedCourse(string Email)
        {
            var purchasedCourses = db.addCourseContents
                     .Where(ac => db.PurchaseOrder.Any(po => po.CourseName == ac.CourseName && po.Email == Email))
                     .ToList();

            return purchasedCourses;
        }

        public List<AddCourseContent> GetSubCourses(string Email, string CourseName)
        {
            // Check if the email and course name are present in the PurchaseOrder table
            bool isPurchased = db.PurchaseOrder
                .Any(po => po.Email == Email && po.CourseName == CourseName);

            if (isPurchased)
            {
                // Retrieve and return the data from AddCourseContent
                var subCourses = db.addCourseContents
                    .Where(acc => acc.CourseName == CourseName)
                    .ToList();

                return subCourses;
            }

            // Return an empty list if the email and course name combination is not found
            return new List<AddCourseContent>();
        }

        // -------------------------------------------------------- Notification 
        public void AddNotification(Notification notification)
        {
           
                db.notifications.Add(notification);
                db.SaveChanges();
         
        }
        public List<Notification> GetNotification()
        {
            var data = db.notifications.ToList();
            return data;
        }

        // -------------------------------------------------------- Promotion 
        public void AddPormotions(Promotion promotion)
        {
            db.promotions.Add(promotion);
            db.SaveChanges();
        }

        public List<Promotion> GetPromotions()
        {
            var data = db.promotions.ToList();
            return data;
        }

        public void SaveProgress(VideoProgress progress)
        {
            var existingProgress = db.VideoProgress.FirstOrDefault(p => p.UserEmail == progress.UserEmail);
            if (existingProgress != null)
            {
                existingProgress.VideoUrl = progress.VideoUrl;
                existingProgress.CurrentTime = progress.CurrentTime;
            }
            else
            {
                db.VideoProgress.Add(progress);
                db.SaveChanges();
            }
        }

        public VideoProgress GetProgress(string email, string Vurl)
        {
            return db.VideoProgress.FirstOrDefault(p => p.UserEmail == email && p.VideoUrl==Vurl);
        }



        // ------------------------------------------ COMMENTS  -------------------------

        public void CreateCommentAsync(Comments comment)
        {
            comment.status = "Pending";
            db.Comments.Add(comment);
            db.SaveChanges();
        }
        public List<Comments> GetApprovedCommentsAsync()
        {
              var data= db.Comments.Where(c => c.status == "Approved").ToList();
              return data;
        }

        public void ApproveCommentAsync(int id)
        {
            var comment = db.Comments.Find(id);
            comment.status = "Approved";
            db.SaveChanges();
        }

        public List<Comments> GetAllcommets()
        {
              var data= db.Comments.Where(c => c.status != "Approved").ToList();
              return data;
        }

        public void DeleteCommentAsync(int id)
		{
			var comment = db.Comments.Find(id);
			db.Comments.Remove(comment);
			db.SaveChanges();
		}

		public List<Comments> GetCommentByCourse(string CourseName)
		{
			   var data = db.Comments.Where(c => c.courseName == CourseName && c.status== "Approved").ToList();
               return data;
		}
        // ------------------------------------------------- User Management
        public void BlockUser(string email)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                if (user.Role == "Blocked")
                {
                    user.Role = "user";
                }
                else
                {
                    user.Role = "Blocked";
                }

                db.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
           var data= db.Users.ToList();
            return data;
        }



		// ------------------------------------------------- Add Review

		public bool AddReview(Review review)
		{
			var existingReview = db.reviews.FirstOrDefault(r => r.CourseName == review.CourseName && r.ReviewerEmail == review.ReviewerEmail);

			if (existingReview != null)
			{
				// Update the existing review's rating
				existingReview.Rating = review.Rating;
			}
			else
			{
				// Add the new review
				db.reviews.Add(review);
			}

			int changes = db.SaveChanges();
			return changes > 0; // Return true if any changes were made to the database
		}

		public double GetReviewByCourse(string courseName)
		{
			var reviews = db.reviews.Where(r => r.CourseName == courseName);

			if (!reviews.Any())
			{
				return 0; // Use double.NaN to indicate no reviews
			}

			return reviews.Average(r => r.Rating);
		}


        // Course Managment
		public void deleteSubCourse(int id)
		{
            // Find the subcourse by id
            var subCourse = db.addCourseContents.FirstOrDefault(c => c.Id == id);
            if (subCourse != null) {    
                // Remove the subcourse
				db.addCourseContents.Remove(subCourse);
				db.SaveChanges();
			}
		}

		public void UpdateSubCourse(AddCourseContent subCourse)
		{
			// Find the subcourse by id
			var existingSubCourse = db.addCourseContents.FirstOrDefault(c => c.Id == subCourse.Id);
			if (existingSubCourse != null)
			{
				// Update the properties of the existing subcourse
				db.Entry(existingSubCourse).CurrentValues.SetValues(subCourse);
				db.SaveChanges();
			}
		}

		public List<AddCourseContent> GetSubCourseBYCorurse(string CourseName)
		{
			var subCourses = db.addCourseContents.Where(c => c.CourseName == CourseName).ToList();
			return subCourses;
		}
	}
}
