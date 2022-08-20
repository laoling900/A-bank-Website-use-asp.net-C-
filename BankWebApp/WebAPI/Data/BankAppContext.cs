using Microsoft.EntityFrameworkCore;
using BankWebAdmin.Models;

namespace BankWebAdmin.Data;

public class BankAppContext : DbContext
{
    public BankAppContext(DbContextOptions<BankAppContext> options) : base(options)
    { }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<login> Logins { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<BillPay> BillPays { get; set; }
    public DbSet<Payee> Payees { get; set; }

}

