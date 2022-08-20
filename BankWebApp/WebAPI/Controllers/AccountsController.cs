using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using BankWebAdmin.Models.DataManager;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountManager _repo;

        public AccountsController(AccountManager repo)
        {
            _repo = repo;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _repo.GetAll();
        }

        // GET api/Accounts/1
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/Accounts
        [HttpPost]
        public void Post([FromBody] Account account)
        {
            _repo.Add(account);
        }

        // PUT api/Accounts
        [HttpPut]
        public void Put([FromBody] Account account)
        {
            _repo.Update(account.AccountNumber, account);
        }

        // DELETE api/Accounts/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
