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
        public DataLoadService service { get; set; }

        public ParkingController(DataLoadService service)
        {
            this.service = service;
        }

        // GET: api/Parking/free
        [HttpGet]
        public int GetFreePlaces()
        {
            return service.parking.GetFreePlacesCount();
        }

        // GET: api/Parking/using
        [HttpGet]
        public int GetUsingPlaces()
        {
            return service.parking.UsingPlacesCount();
        }

        // GET: api/Parking/earned
        [HttpGet]
        public int GetEarnedMoney()
        {
            return service.parking.GetEarnedMoney();
        }
    }
}
