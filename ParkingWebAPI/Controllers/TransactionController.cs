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
        private readonly DataLoadService Service;
        private new ParkingLibrary.Response Response { get; set; }

        public TransactionController(DataLoadService service)
        {
            this.Service = service;
        }

        // GET: api/Transaction
        [HttpGet("GetTransactions")]
        public IActionResult GetTransactions()
        {
            Response = Service.parking.ShowAllTransactions();
            return StatusCode(Response.Status, Response.Data);
        }

        // GET: api/Transaction/GetLastMinuteTransactions
        [HttpGet("GetLastMinuteTransactions")]
        public IActionResult GetLastMinuteTransactions()
        {
            Response = Service.parking.GetLastMinuteTransactions();
            return StatusCode(Response.Status, Response.Data);
        }

        // GET: api/Transaction/GetLastMinuteTransactions/{id}
        [HttpGet("GetLastMinuteTransactions/{id}")]
        public IActionResult GetLastMinuteTransactions(string id)
        {
            Response = Service.parking.GetLastMinuteTransactions(id);
            return StatusCode(Response.Status, Response.Data);
        }

        // PUT: api/Transaction/Balance/{id}/{sum}
        [HttpPut("Balance/{id}/{sum}")]
        public IActionResult Balance(string id, string sum)
        {
            Response = Service.parking.RaiseCarBalance(id, sum);
            return StatusCode(Response.Status, Response.Data);
        }
    }
}
