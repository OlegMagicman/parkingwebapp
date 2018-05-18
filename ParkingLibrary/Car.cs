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
        public CarTypes CarType { get; }
        public enum CarTypes : int { passager, truck, bus, motorcycles };

        public Car(int id, int balance, CarTypes type)
        {
            this.Id = id;
            this.Balance = balance;
            this.CarType = type;
        }

        public void ChangeBalance(int count)
        {
            this.Balance += count;
        }
    }
}
