using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingWebAPI.Services;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private readonly DataLoadService Service;
        private new ParkingLibrary.Response Response { get; set; }

        public CarsController(DataLoadService service)
        {
            this.Service = service;
        }

        // GET: api/Cars
        [HttpGet]
        public IActionResult GetCars()
        {
            Response = Service.parking.GetCarsList();
            return StatusCode(Response.Status, Response.Data);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public IActionResult GetCar(string id)
        {
            Response = Service.parking.GetCar(id);
            return StatusCode(Response.Status, Response.Data);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(string id)
        {
            Response = Service.parking.RemoveCar(id);
            return StatusCode(Response.Status, Response.Data);
        }

        // POST: api/Cars/5
        [HttpPost("{type}")]
        public IActionResult AddCar(string type)
        {
            Response = Service.parking.AddCar(type, null);
            return StatusCode(Response.Status, Response.Data);
        }

        // POST: api/Cars/1/5
        [HttpPost("{type}/{balance}")]
        public IActionResult AddCar(string type, string balance)
        {
            Response = Service.parking.AddCar(type, balance);
            return StatusCode(Response.Status, Response.Data);
        }
    }
}
