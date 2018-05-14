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

        public ParkingController()
        {
            this.service = DataLoadService.Instance;
        }

        // GET: api/Parking/free
        [HttpGet, Route("/api/Parking/free")]
        public int GetFreePlaces()
        {
            return service.parking.GetFreePlacesCount();
        }

        // GET: api/Parking/using
        [HttpGet, Route("/api/Parking/using")]
        public int GetUsingPlaces()
        {
            return service.parking.UsingPlacesCount();
        }

        // GET: api/Parking/earned
        [HttpGet, Route("/api/Parking/earned")]
        public int GetEarnedMoney()
        {
            return service.parking.GetEarnedMoney();
        }
    }
}
