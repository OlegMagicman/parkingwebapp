using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkingLibrary;

namespace ParkingWebAPI.Services
{
    public class DataLoadService
    {
        public Parking parking { get; set; }

        private static readonly Lazy<DataLoadService> lazy = new Lazy<DataLoadService>(() => new DataLoadService());

        public static DataLoadService Instance { get { return lazy.Value; } }

        private DataLoadService()
        {
            parking = new Parking();
            TimerCallback TimerDelegate1 = new TimerCallback(parking.TakeFineFromBalance);
            Timer fineTimer = new Timer(TimerDelegate1, parking, 3000, 3000);

            TimerCallback TimerDelegate2 = new TimerCallback(parking.LogLastMinuteMoney);
            Timer logTimer = new Timer(TimerDelegate2, parking, 60000, 60000);
        }
    }
}
