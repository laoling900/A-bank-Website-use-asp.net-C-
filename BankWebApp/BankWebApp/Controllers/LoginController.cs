using Microsoft.AspNetCore.Mvc;
using BankWebApp.Data;
using BankWebApp.Models;
using SimpleHashing;

namespace BankWebApp.Controllers;

// Bonus Material: Implement global authorisation check.
//[AllowAnonymous]
[Route("/BankWebApp/SecureLogin")]
public class LoginController : Controller
{
    private readonly BankAppContext _context;
    private readonly LoginOps _loginOps;

    //
    public LoginController(BankAppContext context) => _context = context;

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string loginID, string password)
    {
        var login = await _context.Logins.FindAsync(loginID);
        if(login == null || string.IsNullOrEmpty(password) || !PBKDF2.Verify(login.PasswordHash, password))
        { 
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new Login { LoginID = loginID });
        }if(login.LoginState == "Lock")
        {
            ModelState.AddModelError("LoginFailed", "You account has been lock by admin , pls contact with bank admin. ");
            return View(new Login { LoginID = loginID });
        }

        // Login customer.
        HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
        HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);
        HttpContext.Session.SetString(nameof(loginID), loginID);

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
