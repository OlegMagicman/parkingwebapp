using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLibrary
{
    public class CarType
    {
        public int Passager { get; } = 1;
        public int Truck { get; } = 2;
        public int Bus { get; } = 3;
        public int Motorcycles { get; } = 4;
        public Dictionary<int, string> CarList { get; }

        public CarType()
        {
            CarList = new Dictionary<int, string>
            {
                [Passager] = "Passager",
                [Truck] = "Truck",
                [Bus] = "Bus",
                [Motorcycles] = "Motorcycle"
            };
        }

        public Dictionary<int, string> GetCarTypes()
        {
            return CarList;
        }
    }
}
