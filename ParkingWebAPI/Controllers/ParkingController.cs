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
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private Parking parking;

        private ParkingController()
        {
            parking = new Parking();
        }

        // GET: api/Parking/free
        [HttpGet("free/")]
        public int GetFreePlaces()
        {
            return parking.GetFreePlacesCount();
        }

        // GET: api/Parking/using
        [HttpGet("using/")]
        public int GetUsingPlaces()
        {
            return parking.UsingPlacesCount();
        }

        // GET: api/Parking/earned
        [HttpGet("earned/")]
        public int GetEarnedMoney()
        {
            return parking.GetEarnedMoney();
        }
    }
}
