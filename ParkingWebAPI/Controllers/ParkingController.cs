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
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private readonly DataLoadService Service;
        private new ParkingLibrary.Response Response { get; set; }

        public ParkingController(DataLoadService service)
        {
            this.Service = service;
        }

        // GET: api/Parking/free
        [HttpGet("free")]
        public IActionResult GetFreePlaces()
        {
            Response = Service.parking.GetFreePlacesCount();
            return StatusCode(Response.Status, Response.Data);
        }

        // GET: api/Parking/using
        [HttpGet("using")]
        public IActionResult GetUsingPlaces()
        {
            Response = Service.parking.UsingPlacesCount();
            return StatusCode(Response.Status, Response.Data);
        }

        // GET: api/Parking/earned
        [HttpGet("earned")]
        public IActionResult GetEarnedMoney()
        {
            Response = Service.parking.GetEarnedMoney();
            return StatusCode(Response.Status, Response.Data);
        }
    }
}
