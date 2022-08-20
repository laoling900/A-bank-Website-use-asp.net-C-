using BankWebAdmin.Data;
using BankWebAdmin.Models.Repository;


namespace BankWebAdmin.Models.DataManager
{
    //About Get,Add,Delete,Update Payees's data repository.
    public class PayeeManager : IDataRepository<Payee, int>
    {

        private readonly BankAppContext _context;

        public PayeeManager(BankAppContext context)
        {
            _context = context;
        }

        public Payee Get(int id)
        {
            return _context.Payees.Find(id);
        }

        public IEnumerable<Payee> GetAll()
        {
            return _context.Payees.ToList();
        }

        public int Add(Payee payee)
        {
            _context.Payees.Add(payee);
            _context.SaveChanges();

            return payee.PayeeID;
        }

        public int Delete(int id)
        {
            _context.Payees.Remove(_context.Payees.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, Payee payee)
        {
            _context.Update(payee);
            _context.SaveChanges();

            return id;
        }
    }
}
