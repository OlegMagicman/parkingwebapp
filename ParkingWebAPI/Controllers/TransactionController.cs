using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingWebAPI.Services;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]
    public class TransactionController : Controller
    {
        DataLoadService service { get; set; }

        public TransactionController()
        {
            this.service = DataLoadService.Instance;
        }

        // GET: api/Transaction
        [HttpGet, Route("/api/Transaction")]
        public List<Tuple<string, string, string>> Get()
        {
            return service.parking.ShowAllTransactions();
        }

        // GET: api/Transaction/GetLastMinuteTransactions
        [HttpGet, Route("/api/Transaction/GetLastMinuteTransactions")]
        public IEnumerable<object> GetLastMinuteTransactions()
        {
            return service.parking.GetLastMinuteTransactions();
        }

        // GET: api/Transaction/GetLastMinuteTransactions/1
        [HttpGet, Route("/api/Transaction/GetLastMinuteTransactions/{id}")]
        public IEnumerable<object> GetLastMinuteTransactions(int id)
        {
            return service.parking.GetLastMinuteTransactions(id);
        }

        // PUT: api/Transaction/5
        [HttpPut, Route("/api/Transaction/RefillBalance/{id}")]
        public void Put(int id, [FromBody] TransactionPostModel model)
        {
            service.parking.RaiseCarBalance(id, model.sum);
        }
    }

    public class TransactionPostModel
    {
        public int sum { get; set; }
    }
}
