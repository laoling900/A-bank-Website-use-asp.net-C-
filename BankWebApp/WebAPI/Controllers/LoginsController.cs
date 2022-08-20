using Microsoft.AspNetCore.Mvc;
using BankWebAdmin.Models;
using BankWebAdmin.Models.DataManager;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginsController : ControllerBase
    {
        private readonly LoginManager _repo;

        public LoginsController(LoginManager repo)
        {
            _repo = repo;
        }

        // GET: api/logins
        [HttpGet]
        public IEnumerable<login> Get()
        {
            return _repo.GetAll();
        }

        // GET api/logins/1
        [HttpGet("{id}")]
        public login Get(string id)
        {
            return _repo.Get(id);
        }

        // POST api/logins
        [HttpPost]
        public void Post([FromBody] login login)
        {
            _repo.Add(login);
        }

        // PUT api/logins
        [HttpPut]
        public void Put([FromBody] login login)  
        {
            _repo.Update(login.LoginID, login);
        }

        // DELETE api/logins/1
        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            return _repo.Delete(id);
        }

    }
}
