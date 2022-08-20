using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankWebApp.Data;

namespace BankWebApp.Models;
    public class CustomerOps
    {

    //Method when deposit from this customer an account
    public Transaction deposit(Account account ,decimal amount, string comment,BankAppContext context)
    {
        account.Balance += amount;

        Transaction t = new Transaction(TransactionType.Deposit, account.AccountNumber, null, amount, comment);

        account.Transactions.Add(t);
        context.SaveChanges();

        return t;
    }

    //Method when withdraw from this customer an account
    public List<Transaction> withdraw(Account account, decimal amount, string comment, BankAppContext context)
    {
        List<Transaction> transactions = new List<Transaction>();
        Transaction t = new Transaction(TransactionType.Withdraw, account.AccountNumber, null, amount, comment);
        transactions.Add(t);
        account.Transactions.Add(t);
        account.Balance -= amount;
        context.SaveChanges();

        var record = 0;
        foreach(var trans in account.Transactions)
        {
            if (trans.TransactionType.Equals(TransactionType.Withdraw))
            {
                record += 1;
            }else if (trans.TransactionType.Equals(TransactionType.Transfer))
            {
                record += 1;
            }
        }

        if(record > 2)
        {
            account.Balance-=0.05m;
            Transaction serviceChargeTransaction = new Transaction(TransactionType.ServiceCharge, account.AccountNumber, null, 0.05m,null);
            transactions.Add(serviceChargeTransaction);
            account.Transactions.Add(serviceChargeTransaction);
            context.SaveChanges();
        }

        return transactions;
    }
    
    //Method when transfer from this customer   an account   to another account  or other customer any account 
    public List<Transaction> transfer(Account account,Account destinationAccount,  decimal amount, string comment, BankAppContext context)
    {
        List<Transaction> transactions = new List<Transaction>();
        Transaction transaction  = new Transaction(TransactionType.Transfer, account.AccountNumber, destinationAccount.AccountNumber, amount, comment);
        Transaction destinationTransaction = new Transaction(TransactionType.Transfer, destinationAccount.AccountNumber, null, amount, comment);
        transactions.Add(transaction);
        transactions.Add(destinationTransaction);
        
        account.Transactions.Add(transaction);
        destinationAccount.Transactions.Add(destinationTransaction);
        
        account.Balance -= amount;
        destinationAccount.Balance += amount;
        context.SaveChanges();

        var record = 0;
        foreach (var trans in account.Transactions)
        {
            if (trans.TransactionType.Equals(TransactionType.Withdraw))
            {
                record += 1;
            }
            else if (trans.TransactionType.Equals(TransactionType.Transfer))
            {
                record += 1;
            }
        }

        if (record > 2)
        {
            account.Balance -= 0.05m;
            Transaction serviceChargeTransaction = new Transaction(TransactionType.ServiceCharge, account.AccountNumber, null, 0.05m, null);
            transactions.Add(serviceChargeTransaction);
            account.Transactions.Add(serviceChargeTransaction);
            context.SaveChanges();
        }

        return transactions;
    }


    public void Profile(Customer customer, string name, string TFN, string address, string suburb, string state, string postcode, string mobile,  BankAppContext context)
    {
        customer.Name = name;
        customer.TFN = TFN;
        customer.Address=address;
        customer.Suburb = suburb;
        customer.State = state;
        customer.Postcode=postcode;
        customer.Mobile=mobile;

        context.SaveChanges();

    }

    public void ChangePassword(Login login,string hash, BankAppContext context)
    {
        login.PasswordHash =hash;
        context.SaveChanges();
    }

}

