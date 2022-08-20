using BankWebAdmin.Data;
using BankWebAdmin.Models.Repository;


namespace BankWebAdmin.Models.DataManager
{
    //About Get,Add,Delete,Update Accounts's data repository.
    public class AccountManager : IDataRepository<Account, int>
    {

        private readonly BankAppContext _context;

        public AccountManager(BankAppContext context)
        {
            _context = context;
        }

        public Account Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public int Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account.AccountNumber;
        }

        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Account account)
        {
            _context.Update(account);
            _context.SaveChanges();

            return id;
        }
    }
}