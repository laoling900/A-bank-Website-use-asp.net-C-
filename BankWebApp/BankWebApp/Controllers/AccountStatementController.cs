using Microsoft.AspNetCore.Mvc;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.Filters;
using X.PagedList;

namespace BankWebApp.Controllers
{
    [AuthorizeCustomer]
    public class AccountStatementController : Controller
    {
        private readonly BankAppContext _context;
        private readonly AccountOps _accountOps;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;

        public AccountStatementController(BankAppContext context, AccountOps accountOps)
        {
            this._context = context;
            this._accountOps = accountOps;
        }

        public async Task<IActionResult> Statement()
        {
            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customers.FindAsync(CustomerID);

            return View(customer);
        }

         [HttpPost]
         //Get Statement1
        public async Task<IActionResult> IndexToViewStatement(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return NotFound();

            return RedirectToAction(nameof(ViewStatement));
        }
        //Get Statement1
        public async Task<IActionResult> ViewStatement(int id, int? page =1)
        {
            var account = await _context.Accounts.FindAsync(id);
            

            if (account == null)
            {
                return RedirectToAction(nameof(Statement));
            }
            ViewBag.Account = account;
            const int pageSize = 4;
            var pagedList = await _context.Transactions.Where(x => x.AccountNumber == account.AccountNumber).OrderBy(x => x.TransactionTimeUtc).ToPagedListAsync(page, pageSize);


            return View(pagedList);

        }



    }
}
