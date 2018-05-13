using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLibrary
{
    public class Car
    {
        public int Id { get; }
        public int Balance { get; private set; }
        public int CarType { get; }

        public Car(int id, int carType)
        {
            this.Id = id;
            this.Balance = 0;
            this.CarType = carType;
        }

        public void ChangeBalance(int count, bool sign)
        {
            if (sign == true)
                this.Balance += count;
            else
                this.Balance -= count;
        }
    }
}
