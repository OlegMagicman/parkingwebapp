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
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        public DataLoadService service { get; set; }

        public CarsController(DataLoadService service)
        {
            this.service = service;
        }

        // GET: api/Cars
        [HttpGet]
        public IEnumerable<object> GetCars()
        {
            return service.parking.GetCarsList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public object GetCar(string id)
        {
            return service.parking.GetCar(id);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public void DeleteCar(string id)
        {
            service.parking.RemoveCar(id);
        }

        // POST: api/Cars/5
        [HttpPost("{cartype}")]
        public void AddCar(string cartype)
        {
            service.parking.AddCar(cartype);
        }
    }
}
