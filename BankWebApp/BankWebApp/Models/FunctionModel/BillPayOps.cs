using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankWebApp.Models;

    public class BillPayOps
    {
    public BillPay createBillPay(Account account, decimal amount, DateTime dateTime, BankAppContext context)
    {

        //need payeeID
        int payeeID = 0;
        List<Payee> payees = context.Payees.ToList();
        foreach (var payee in payees)
        {
            if (payee.Name == payeeName)
            {
                payeeID = payee.PayeeID;
            }
        }

        //need Billpayperid
        if (period == "Once")
        {
            period = "O";
        }
        else if (period == "Monthly")
        {
            period = "M";
        }

        BillPay billPay = new BillPay(account.AccountNumber, payeeID, amount, dateTime, period, BillPayType.Pending);
        account.BillPays.Add(billPay);
        //context.BillPays.Add(billPay);
        this.billPay = billPay;
        context.SaveChanges();

        return billPay;
    }
    //Judge which BillPayPeriod 
    public BillPay modifyBillPay(BillPay billPay, decimal amount, DateTime dateTime, string period, BankAppContext context)
    {
        billPay.Amount = amount;
        billPay.ScheduleTimeUtc = dateTime;
        if (period == "Once")
        {
            billPay.BillPayPeriod = "O";
        }
        else if (period == "Monthly")
        {
            billPay.BillPayPeriod = "M";
        }
        context.SaveChanges();

        return billPay;
    }
    //change BillPayType
    public Boolean exceuteBillPay(BillPay billPay, BankAppContext context ,AccountOps accountOps)
    {

        if (billPay.ScheduleTimeUtc < DateTime.Now && billPay.BillPayType.Equals(BillPayType.Pending) && billPay.LockState == null)

        {
            Account account = context.Accounts.Find(billPay.AccountNumber);
            if (accountOps.enoughBalance(account, billPay.Amount))
            {
                account.Balance -= billPay.Amount;
                billPay.BillPayType = BillPayType.Finish;
                context.SaveChanges();
                return true;
            }
            else
            {
                billPay.BillPayType = BillPayType.Decline;
                return false;
            }

        }
        return false;
    }
    
    public BillPay nextMonthBillPay(BillPay billPay,BankAppContext context)
    {
        BillPay nextMonthBillPay = new BillPay(billPay.AccountNumber, billPay.PayeeID, billPay.Amount, billPay.ScheduleTimeUtc.AddMonths(1), "M", BillPayType.Pending);
        Account account = context.Accounts.Find(billPay.AccountNumber);
        account.BillPays.Add(nextMonthBillPay);
        
        context.SaveChanges();
        return nextMonthBillPay;
    }

    public SelectList PayeeNames { get; set; }
    public string payeeName { get; set; }

    public SelectList PeriodList { get; set; }
    public BillPay billPay { get; set;  }

    public string? period { get; set; }


}

