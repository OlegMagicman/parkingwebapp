using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLibrary
{
    public sealed class Settings
    {
        private static readonly Lazy<Settings> lazy = new Lazy<Settings>(() => new Settings());

        public int timeout;
        public Dictionary<Car.CarTypes, int> prices;
        public int totalSpace;
        public int fine;
        public string filePath;

        public static Settings Instance { get { return lazy.Value; } }

        private Settings()
        {
            timeout = 3000;

            prices = new Dictionary<Car.CarTypes, int>
            {
                [Car.CarTypes.passager] = 3,
                [Car.CarTypes.truck] = 5,
                [Car.CarTypes.bus] = 2,
                [Car.CarTypes.motorcycles] = 1
            };

            totalSpace = 100;

            fine = 2;

            filePath = "Transaction.log";
        }
    }
}
