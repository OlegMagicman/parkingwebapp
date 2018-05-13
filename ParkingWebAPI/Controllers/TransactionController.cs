using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLibrary;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]
    public class TransactionController : Controller
    {
        Parking parking;
        public TransactionController()
        {
            parking = new Parking();
        }

        // GET: api/Transaction
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return parking.ShowAllTransactions();
        }

        // GET: api/Transaction/5
        [HttpGet("GetLastMinuteTransactions")]
        public IEnumerable<Transaction> GetLastMinuteTransactions()
        {
            return parking.GetLastMinuteTransactions();
        }

        // GET: api/Transaction/5
        [HttpGet("GetLastMinuteTransactions/{id}")]
        public IEnumerable<Transaction> GetLastMinuteTransactions(int id)
        {
            return parking.GetLastMinuteTransactions(id);
        }

        // PUT: api/Transaction/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]int sum)
        {
            parking.RaiseCarBalance(id, sum);
        }
    }
}
