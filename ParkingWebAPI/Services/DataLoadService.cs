using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkingLibrary;

namespace ParkingWebAPI.Services
{
    public sealed class DataLoadService
    {
        public Parking parking { get; set; }

        private static readonly Lazy<DataLoadService> lazy = new Lazy<DataLoadService>(() => new DataLoadService());

        public static DataLoadService Instance { get { return lazy.Value; } }

        public DataLoadService()
        {
            this.parking = Parking.Instance;
        }
    }
}
