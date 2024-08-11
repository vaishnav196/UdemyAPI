using Microsoft.AspNetCore.Mvc;
using UdemyAPI.Ecrypt;
using UdemyAPI.Models;
using UdemyAPI.Repo;

namespace UdemyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositry repo;
        public AuthController(IRepositry repo)
        {
            this.repo = repo;
        }

          [HttpPost]
        [Route("AddUser")]
        public IActionResult AddnewUser(User u)
        {
            // Encrypt the password before storing it
            u.Password = EncryptionHelper.Encrypt(u.Password);

            // Attempt to create the user
            bool userCreated = repo.CreateUser(u);

            if (!userCreated)
            {
                return BadRequest("Email already exists.");
            }

            return Ok("User added successfully.");
        }



        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string password)
        {
            try
            {
                User user = repo.GetUserByEmail(Email);

                if (user != null)
                {
                    if (user.Role == "Blocked")
                    {
                        return Unauthorized("Access denied: Your account is blocked.");
                    }

                    // Decrypt the stored password
                    string decryptedPassword = EncryptionHelper.Decrypt(user.Password);

                    if (password == decryptedPassword)
                    {
                        // Authentication successful
                        return Ok(user);
                    }
                    else
                    {
                        // Authentication failed
                        return Unauthorized("Invalid credentials");
                    }
                }
                else
                {
                    // User not found
                    return Unauthorized("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<User> users = repo.GetAllUsers();
            return Ok(users);
        }

    }
}
