using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLibrary
{
    public class Transaction
    {
        public DateTime DateTime { get; }
        public int CarId { get; }
        public int Cash { get; }

        public Transaction(int CarId, int Cash)
        {
            this.CarId = CarId;
            this.Cash = Cash;
            this.DateTime = DateTime.Now;
        }
    }
}
