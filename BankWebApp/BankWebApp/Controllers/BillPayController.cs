using Microsoft.AspNetCore.Mvc;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankWebApp.Controllers;

[AuthorizeCustomer]
public class BillPayController : Controller
{
    private readonly BankAppContext _context;
    private readonly BillPayOps _billPayOps;
    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;


    public BillPayController(BankAppContext context, BillPayOps billPayOps)
    {
        this._context = context;
        this._billPayOps = billPayOps;
    }
    //Get BillPay/Index
    public async Task<IActionResult> Index()
    {
        var customer = await _context.Customers.FindAsync(CustomerID);
        return View(customer);
    }
    //Get BillPay/ViewBillPay
    public async Task<IActionResult> ViewBillPay(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        List<BillPay> billPays = account.BillPays;
        return View(billPays);

    }
    //Get BillPay/ModifyBillPay
    public async Task<IActionResult> ModifyBillPay(int id)
    {
        var billPay = await _context.BillPays.FindAsync(id);
        return View(billPay);
    }
    
    //Get BillPay/CreateBillPay
    public async Task<IActionResult> CreateBillPay(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        ViewBag.AccountNumber = account.AccountNumber;
        var payeeNameList = _context.Payees.Select(x => x.Name);
        var periodList = new List<string> { "Once", "Monthly" };

        return View(new BillPayOps
        {
            payeeName = "1",
            PayeeNames = new SelectList(payeeNameList),
            period = "1",
            PeriodList = new SelectList(periodList)
        });

    }
    //Post BillPay/CreateBillPay                                                                                                                                                   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBillPay(int id, BillPayOps billPayOps, string payeeName, string period)
    {
        var account = await _context.Accounts.FindAsync(id);
        ViewBag.AccountNumber = account.AccountNumber;


        //if (billPayOps.billPay.Amount <= 0)
        //{
        //    ModelState.AddModelError("billPay.Amount", "Amount must be positive.");
        //}
        //if (billPayOps.billPay.ScheduleTimeUtc <= DateTime.Now)
        //{
        //    ModelState.AddModelError("billPay.ScheduleTimeUtc", "Can't set a passed time. ");
        //}
        //if (!ModelState.IsValid)
        //{
        //    var payeeNameList = _context.Payees.Select(x => x.Name);
        //    var periodList = new List<string> { "Once", "Monthly" };
        //    billPayOps.PayeeNames = new SelectList(payeeNameList);
        //    billPayOps.PeriodList = new SelectList(periodList);
        //    payeeName = "1";
        //    period = "1";
        //    return View(billPayOps);
        //}
        billPayOps.createBillPay(account, billPayOps.billPay.Amount, billPayOps.billPay.ScheduleTimeUtc, _context);
        return View("ConfirmationBillPayAction", billPayOps.billPay);
    }



    [HttpPost]
    public async Task<IActionResult> DeleteBillPay(int id)
    {
        var billPay = await _context.BillPays.FindAsync(id);
        billPay.BillPayType = BillPayType.Delete;
        _context.SaveChanges();

        return View("ConfirmationBillPayAction", billPay);
    }

    [HttpPost]
    public async Task<IActionResult> ModifyBillPay(int id, decimal amount, DateTime dateTime, string period)
    {
        var billPay = await _context.BillPays.FindAsync(id);
        //get and validation  modify values 
        billPay = _billPayOps.modifyBillPay(billPay, amount, dateTime, period, _context);

        return View("ConfirmationBillPayAction", billPay);
    }

}








