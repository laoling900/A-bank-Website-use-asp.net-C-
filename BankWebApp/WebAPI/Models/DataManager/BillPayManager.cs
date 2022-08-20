using BankWebAdmin.Data;
using BankWebAdmin.Models.Repository;


namespace BankWebAdmin.Models.DataManager
{
    //About Get,Add,Delete,Update BillPays's data repository.
    public class BillPayManager : IDataRepository<BillPay, int>
    {

        private readonly BankAppContext _context;

        public BillPayManager(BankAppContext context)
        {
            _context = context;
        }

        public BillPay Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        public IEnumerable<BillPay> GetAll()
        {
            return _context.BillPays.ToList();
        }

        public int Add(BillPay bill)
        {
            _context.BillPays.Add(bill);
            _context.SaveChanges();

            return bill.BillPayId;
        }

        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();

            return id;
        }

        public int Update(int id, BillPay bill)
        {
            _context.Update(bill);
            _context.SaveChanges();

            return id;
        }
    }
}