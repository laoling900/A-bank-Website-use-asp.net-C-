using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using SimpleHashing;

namespace BankWebAdmin.Controllers
{
    [Route("/BankWebAdmin/SecureLogin")]
    public class LoginController : Controller
    {

        public IActionResult Login() => View();

        //Get Login
        [HttpPost]
        public async Task<IActionResult> Login(string loginID, string password)
        {
            if (loginID != "Admin" || password != "Admin")
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
                return View(new LoginDto { LoginID = loginID });

            }
            HttpContext.Session.SetInt32(nameof(CustomerDto.CustomerID), 1);

            return RedirectToAction("Index", "Customer");
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}