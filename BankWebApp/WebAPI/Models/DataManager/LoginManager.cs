using BankWebAdmin.Data;
using BankWebAdmin.Models.Repository;


namespace BankWebAdmin.Models.DataManager
{
    // //About Get,Add,Delete,Update Logins's data repository.
    public class LoginManager : IDataRepository<login, string>
    {

        private readonly BankAppContext _context;

        public LoginManager(BankAppContext context)
        {
            _context = context;
        }

        public login Get(string id)
        {
            return _context.Logins.Find(id);
        }

        public IEnumerable<login> GetAll()
        {
            return _context.Logins.ToList();
        }

        public string Add(login login)
        {
            _context.Logins.Add(login);
            _context.SaveChanges();

            return login.LoginID;
        }

        public string Delete(string id)
        {
            _context.Logins.Remove(_context.Logins.Find(id));
            _context.SaveChanges();

            return id;
        }

        public string Update(string id, login login)
        {
            _context.Update(login);
            _context.SaveChanges();

            return id;
        }
    }
}