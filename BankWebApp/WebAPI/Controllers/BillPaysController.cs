using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using BankWebAdmin.Models.DataManager;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BillPaysController : ControllerBase
    {
        private readonly BillPayManager _repo;

        public BillPaysController(BillPayManager repo)
        {
            _repo = repo;
        }

        // GET: api/billpays
        [HttpGet]
        public IEnumerable<BillPay> Get()
        {
            return _repo.GetAll();
        }

        // GET api/billpays/1
        [HttpGet("{id}")]
        public BillPay Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/billpays
        [HttpPost]
        public void Post([FromBody] BillPay bill)
        {
            _repo.Add(bill);
        }

        // PUT api/billpays
        [HttpPut]
        public void Put([FromBody] BillPay bill)
        {
            _repo.Update(bill.BillPayId, bill);
        }

        // DELETE api/billpays/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}
