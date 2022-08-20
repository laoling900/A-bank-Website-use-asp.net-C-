using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using BankWebAdmin.Models.DataManager;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionManager _repo;

        public TransactionsController(TransactionManager repo)
        {
            _repo = repo;
        }

        // GET: api/transactions
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _repo.GetAll();
        }

        // GET api/transactions/1
        [HttpGet("{id}")]
        public Transaction Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/transactions
        [HttpPost]
        public void Post([FromBody] Transaction transaction)
        {
            _repo.Add(transaction);
        }

        // PUT api/transactions
        [HttpPut]
        public void Put([FromBody] Transaction transaction)
        {
            _repo.Update(transaction.TransactionID, transaction);
        }

        // DELETE api/transactions/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
