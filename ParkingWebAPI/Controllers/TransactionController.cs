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

        public TransactionController(DataLoadService service)
        {
            this.service = service;
        }

        // GET: api/Transaction
        [HttpGet]
        public List<Tuple<string, string, string>> GetTransactions()
        {
            return service.parking.ShowAllTransactions();
        }

        // GET: api/Transaction/GetLastMinuteTransactions
        [HttpGet]
        public IEnumerable<object> GetLastMinuteTransactions()
        {
            return service.parking.GetLastMinuteTransactions();
        }

        // GET: api/Transaction/GetLastMinuteTransactions/{id}
        [HttpGet("{id}")]
        public IEnumerable<object> GetLastMinuteTransactions(int id)
        {
            return service.parking.GetLastMinuteTransactions(id);
        }

        // PUT: api/Transaction/Balance/{id}/{sum}
        [HttpPut("{id}/{sum}")]
        public void Balance(int id, int sum)
        {
            service.parking.RaiseCarBalance(id, sum);
        }
    }
}
