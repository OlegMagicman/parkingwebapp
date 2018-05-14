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

        public CarsController()
        {
            this.service = DataLoadService.Instance;
        }

        // GET: api/Cars
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return service.parking.GetCarsList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return service.parking.GetCar(id);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.parking.RemoveCar(id);
        }

        // POST: api/Cars
        [HttpPost]
        public void Post([FromBody] CarPostModel model)
        {
            service.parking.AddCar(model.cartype);
        }
    }

    public class CarPostModel
    {
        public int cartype { get; set; }
    }
}
