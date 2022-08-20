using Microsoft.AspNetCore.Mvc;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.Utilities;
using BankWebApp.Filters;
using SimpleHashing;

namespace BankWebApp.Controllers;

// Can add authorize attribute to controllers.
[AuthorizeCustomer]
public class CustomerController : Controller
{
    private readonly BankAppContext _context;
    private readonly CustomerOps _customerOps;
    private readonly AccountOps _accountOps;

    // ReSharper disable once PossibleInvalidOperationException
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
    private string loginID => HttpContext.Session.GetString(nameof(loginID));
    public CustomerController(BankAppContext context, CustomerOps customerOps ,AccountOps accountOps) {
        this._context = context;
        this._customerOps = customerOps;
        this._accountOps = accountOps;
    }

    // Can add authorize attribute to actions.
    //[AuthorizeCustomer]
    //The Index page show  the customer accounts with ATM buttom 
    public async Task<IActionResult> Index()
    {
        var customer = await _context.Customers.FindAsync(CustomerID);
        return View(customer);
    }
    //The page that customer can change his profile 
    public async Task<IActionResult> Profile()
    {
        var customer = await _context.Customers.FindAsync(CustomerID);
        return View(customer);
    }

    //The profile page has an indival  buttom  , go to the page change the password
    public async Task<IActionResult> ChangePassWord()
    {
        var customer = await _context.Customers.FindAsync(CustomerID);
        return View(customer);
    }

    //the Deposit  page when click  deposit  on the My account page
    public async Task<IActionResult> Deposit(int id) => View(await _context.Accounts.FindAsync(id));
    //the withdraw  page when click  withdraw  on the My account page
    public async Task<IActionResult> Withdraw(int id) => View(await _context.Accounts.FindAsync(id));
    //the Transfer  page when click Transfer  on the My account page
    public async Task<IActionResult> Transfer(int id) => View(await _context.Accounts.FindAsync(id));
    // the  My statement page when click the my statement on navgation bar 
    public async Task<IActionResult> Statement(int id) => View(await _context.Accounts.FindAsync(id));

    //
    [HttpPost]
    public async Task<IActionResult> Deposit(int id, decimal amount, string? comment)
    {
        var account = await _context.Accounts.FindAsync(id);

        if(amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if(amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (comment !=null && comment.Length > 30)
        {
            ModelState.AddModelError(nameof(comment), "Comment cannot more than 30 characters.");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            ViewBag.Comment = comment;
            return View(account);
        }

        Transaction t = _customerOps.deposit(account, amount, comment,_context);
        List<Transaction> transactions = new List<Transaction>();
        transactions.Add(t);
        
        return View("ConfirmationATM", transactions);
    }
    // Customer/Withdraw
    [HttpPost]
    public async Task<IActionResult> Withdraw(int id, decimal amount, string? comment)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (comment != null &&  comment.Length > 30)
        {
            ModelState.AddModelError(nameof(comment), "Comment cannot more than 30 characters.");
        }
        if(!_accountOps.enoughBalance(account, amount))
        {
            ModelState.AddModelError(nameof(amount), "Not enough balance in this account. ");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            ViewBag.Comment = comment;
            return View(account);
        }
        
        List<Transaction> transactions = _customerOps.withdraw(account, amount, comment, _context);

        return View("ConfirmationATM", transactions);
    }
    //Customer/Transfer
    [HttpPost]
    public async Task<IActionResult> Transfer(int id, int destAccountNum, decimal amount, string? comment)
    {
        var account = await _context.Accounts.FindAsync(id);
        // try search  destination Account 
        var destinationAccount = await _context.Accounts.FindAsync(destAccountNum);
 
        if(destAccountNum == id)
        {
            ModelState.AddModelError(nameof(destAccountNum), "You cannot transfer to same account. ");
        }
        if (destinationAccount == null)
        {
            ModelState.AddModelError(nameof(destAccountNum), "Invalid Destination Account, pls re-enter.");
        }
        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.HasMoreThanTwoDecimalPlaces())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (!_accountOps.enoughBalance(account, amount))
        {
            ModelState.AddModelError(nameof(amount), "Not enough balance in this account. ");
        }
        if (comment != null &&  comment.Length > 30)
        {
            ModelState.AddModelError(nameof(comment), "Comment cannot more than 30 characters.");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Account = destAccountNum;
            ViewBag.Amount = amount;
            ViewBag.Comment = comment;
            return View(account);
        }

        List<Transaction> transactions = _customerOps.transfer(account, destinationAccount, amount, comment, _context);

        return View("ConfirmationATM", transactions);
    }


    //Edit profile
     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Profile(string name, string? TFN, string?address, string?suburb, string?state, string?postcode, string?mobile)
     {

        var customer = await _context.Customers.FindAsync(CustomerID);
        if (name.Length >50)
            ModelState.AddModelError(nameof(name), "Customername cannot more than 30 characters.");
        if(TFN!=null&&TFN.Length>11)
            ModelState.AddModelError(nameof(TFN), "TFN cannot more than 11 characters.");
        if(address!=null&&address.Length >50)
            ModelState.AddModelError(nameof(address), "Address cannot more than 50 characters.");
        if(suburb!=null&&suburb.Length>40)
            ModelState.AddModelError(nameof(suburb), "Suburb cannot more than 40 characters.");
        if(state!=null&&state.Length!=3&&state.Length!=2)
            ModelState.AddModelError(nameof(state), "State must be 2 or 3 characters.");
        if(postcode!=null&&postcode.Length!=4)
            ModelState.AddModelError(nameof(postcode), "Postcode must be 4 characters.");
        if (mobile != null)
        {
            char[] first = { mobile[0] };
            char[] second = { mobile[1] };
            string firstOne=new string(first);
            string secondOne=new string(second);
            if(firstOne!="0"||secondOne!="4"||mobile.Length!=12)
            ModelState.AddModelError(nameof(mobile), "Mobile must be 12 characters and must be of the format:04xx xxxx xxxx.");
        }
           

        if (!ModelState.IsValid)
        {
            ViewBag.Name = name;
            ViewBag.TFN=TFN;
            ViewBag.Address=address;
            ViewBag.Suburb=suburb;
            ViewBag.State=state;
            ViewBag.Postcode=postcode;
            ViewBag.Mobile=mobile;
            return View(customer);
        }
        _customerOps.Profile(customer, name,TFN, address, suburb, state, postcode, mobile, _context);


        return RedirectToAction("Profile");
    }
   
    //Customer page my profit
    //ChangePassword 
    [HttpPost]
    public async Task<IActionResult> ChangePassWord(string OldPassword, string NewPassword1, string NewPassword2)
    { 
        var login = await _context.Logins.FindAsync(loginID);
       
       
        if (OldPassword == null || NewPassword1 == null || NewPassword2 == null)
        {
            ModelState.AddModelError(nameof(OldPassword), "This filed cannot null, please try again");
        }
        if (OldPassword !=null&& NewPassword1!=null &&NewPassword2 != null){
            if (!PBKDF2.Verify(login.PasswordHash, OldPassword))
            {
                ModelState.AddModelError(nameof(OldPassword), "Password is wrong,please try again.");
            }
            if(NewPassword1 != NewPassword2)
            {
                ModelState.AddModelError(nameof(NewPassword2), " You must enter the same new password, please try again");
            }

        }
            
         
        if (!ModelState.IsValid)
        {
            ViewBag.OldPassword =OldPassword;
            ViewBag.NewPassword1 =NewPassword1;
            ViewBag.NewPassword2 =NewPassword2;
            return View(login);
        }
        string password = NewPassword1;
        string hash = PBKDF2.Hash(password);
        _customerOps.ChangePassword(login, hash, _context);

        return RedirectToAction("ChangePassword");

    }

}
