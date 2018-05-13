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
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private Parking parking;

        private CarsController()
        {
            parking = new Parking();
        }

        // GET: api/Cars
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return parking.GetCarsList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "Get")]
        public Car Get(int id)
        {
            return parking.GetCar(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            parking.RemoveCar(id);
        }

        // POST: api/Cars
        [HttpPost]
        public void Post([FromBody]int balance, [FromBody]int type)
        {
            parking.AddCar(balance, type);
        }
    }
}
