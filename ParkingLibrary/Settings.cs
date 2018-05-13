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
        public Dictionary<int, int> prices;
        public int totalSpace;
        public int fine;
        public string filePath;
        public CarType carType;

        public static Settings Instance { get { return lazy.Value; } }

        private Settings()
        {
            carType = new CarType();

            timeout = 3;

            prices = new Dictionary<int, int>
            {
                [carType.Passager] = 3,
                [carType.Truck] = 5,
                [carType.Bus] = 2,
                [carType.Motorcycles] = 1
            };

            totalSpace = 100;

            fine = 2;

            filePath = "Transaction.log";
        }
    }
}
