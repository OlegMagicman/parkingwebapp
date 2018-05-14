using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLibrary
{
    public class Parking
    {
        public List<Car> CarList { get; private set; }
        public int LastAddedCarId { get; private set; }
        public List<Transaction> TransactionList { get; private set; }
        public int EarnedMoney { get; private set; }
        public int LastMinuteMoney { get; private set; }
        public Settings Settings { get; }
        public CarType CarType { get; }

        public Parking()
        {
            CarList = new List<Car>();
            LastAddedCarId = 0;
            TransactionList = new List<Transaction>();
            EarnedMoney = 0;
            LastMinuteMoney = 0;
            Settings = Settings.Instance;
            CarType = new CarType();
        }

        public void AddCar(int type)
        {
            if (GetFreePlacesCount() == 0)
            {
                Console.WriteLine("No enough free space");
                return;
            }
            LastAddedCarId++;
            var car = new Car(LastAddedCarId, type);
            CarList.Add(car);
        }

        public void RemoveCar(int id)
        {
            try
            {
                var car = CarList.SingleOrDefault(c => c.Id == id);
                if (car != null || car.Balance >= 0)
                    CarList.Remove(car);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Car ID has not defined");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Choose correct ID");
            }
        }

        public List<Car> GetCarsList()
        {
            foreach (var c in CarList)
            {
                Console.WriteLine("{0} CarType: {1}, Balance: {2}", c.Id, c.CarType, c.Balance);
            }
            return CarList;
        }

        public Car GetCar(int id)
        {
            return CarList.SingleOrDefault(c => c.Id == id);
        }

        public Car RaiseCarBalance(int id, int sum)
        {
            try
            {
                var car = CarList.SingleOrDefault(c => c.Id == id);
                car.ChangeBalance(sum, true);
                Console.WriteLine("Balance for car with id {0} was raised by {1}", id, sum);
                TransactionList.Add(new Transaction(id, sum));
                return car;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Something went wrong");
            }
            return null;
        }

        public void TakeFineFromBalance(object StateObj)
        {
            Parking State = (Parking)StateObj;
            var sum = 0;
            int price = 0;
            foreach (var car in State.CarList)
            {
                price = Settings.prices[car.CarType];
                sum = (car.Balance < 0) ? price * Settings.fine : price;

                car.ChangeBalance(sum, false);
                State.TransactionList.Add(new Transaction(car.Id, sum * (-1)));
                State.EarnedMoney += sum;
                State.LastMinuteMoney += sum;
            }
        }

        public int GetFreePlacesCount()
        {
            return Settings.totalSpace - CarList.Count();
        }

        public int UsingPlacesCount()
        {
            return CarList.Count();
        }

        public int GetEarnedMoney()
        {
            return EarnedMoney;
        }

        public List<Transaction> GetLastMinuteTransactions()
        {
            return (from transaction in TransactionList
                    where transaction.DateTime.AddMinutes(1).Minute >= DateTime.Now.Minute
                    select transaction)
                    .ToList();
        }

        public List<Transaction> GetLastMinuteTransactions(int id)
        {
            return (from transaction in TransactionList
                    where transaction.DateTime.AddMinutes(1).Minute >= DateTime.Now.Minute
                    select transaction)
                    .ToList();
        }

        public void LogLastMinuteMoney(object StateObj)
        {
            Parking State = (Parking)StateObj;
            string log = DateTime.Now.ToString("MM.dd.yyyy HH:mm ") + State.LastMinuteMoney;
            try
            {
                using (StreamWriter sw = new StreamWriter(Settings.filePath, true, Encoding.Default))
                {
                    sw.WriteLine(log);
                }
                State.LastMinuteMoney = 0;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public List<Tuple<string, string, string>> ShowAllTransactions()
        {
            List<Tuple<string, string, string>> data = new List<Tuple<string, string, string>>();
            try
            {
                using (StreamReader sw = new StreamReader(Settings.filePath, Encoding.Default))
                {
                    string line;
                    while ((line = sw.ReadLine()) != null)
                    {
                        string[] token = new string[3];
                        token = line.Split(" ");
                        data.Add(Tuple.Create(token[0], token[1], token[2]));
                    }
                }
                return data;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found!");
            }
            catch (FileLoadException)
            {
                Console.WriteLine("File not found!");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            return new List<Tuple<string, string, string>>();
        }
    }
}
