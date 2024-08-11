using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using UdemyAPI.Ecrypt;
using UdemyAPI.Models;
using UdemyAPI.Models.Cart;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly IRepositry repo;
        public CartController(IRepositry repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(Cart c)
        {
            repo.AddCart(c);
            return Ok("Cource added Sucessfully");
        }

        [HttpGet]
        [Route("FetchCart")]
        public IActionResult FetchCart(string Email)
        {
            var data = repo.GetCartData(Email);
            return Ok(data);
        }


        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public IActionResult DeleteItem(int id)
        {
            repo.RemoveCart(id);
            return Ok("Cart remove sucessfully");
        }



        [HttpDelete]
        [Route("ClearCart")]
        public IActionResult ClearAllCart(string Email)
        {
            repo.ClearCart(Email);
            return Ok("ALL Item Remove Sucessfully");
        }


        [HttpPost]
        [Route("ProceedToOrder")]
        public IActionResult ProceedToOrder(string Email)
        {
            repo.ProcessPayment(Email);
            return Ok("Your Order has been Done");
        }

        [HttpGet]
        [Route("FetchOrderedDetails")]
        public IActionResult FetchOrderedDetails()
        {
            var data = repo.GetPurchaseOrderData();
            return Ok(data);
        }

        // If User wants to See Cource 

        [HttpGet]
        [Route("GetCourseByUser")]   // No Use of this api (Delete before Deployement)
        public IActionResult GetCourseByUser(string email, string instructor, string courseName)
        {
            var data = repo.GetPurchasedData(email, instructor, courseName);
            return Ok(data);
        }
         


        // -------------------------------------------- SUB COURSE DATA BY USER FROM PURCHASED TABLE AND ORDER HISTORY
        [HttpGet]
        [Route("VideoContent")] // No Use of this api (Delete before Deployement)
        public IActionResult VideoContent(string Email)
        {
            var data = repo.GetPurchasedCourse(Email);
            return Ok(data);
        }


        [HttpGet]
        [Route("Order_History")]   // USE FOR SHOWING INVOICE AND FOR LINK TO SUB COURUSE
        public IActionResult Order_History(string Email)
        {
            var data = repo.OrderHistory(Email);
            return Ok(data);
        }

        [HttpGet]
        [Route("GetSubCourseAuth")]  // GET SUB COURSE RESOURCES BASED ON EMAIL OF USER RSPECT TO 
        public IActionResult GetSubCourseAuth(string Email,string CourseName)
        {
           var data=repo.GetSubCourses(Email, CourseName);
            //return Ok($"{CourseName} {data}");
            return Ok(data);
        }


    }
}
