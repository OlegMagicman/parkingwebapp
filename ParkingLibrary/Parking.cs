using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ParkingLibrary
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking(Settings.Instance));

        public static Parking Instance { get { return lazy.Value; } }

        private List<Car> CarList { get; set; }
        private List<Transaction> TransactionList { get; set; }
        private Settings Settings { get; }
        private int EarnedMoney { get; set; }
        private int LastAddedCarId { get; set; }
        private int LastMinuteMoney { get; set; }

        private Parking(Settings settings)
        {
            CarList = new List<Car>();
            LastAddedCarId = 0;
            TransactionList = new List<Transaction>();
            EarnedMoney = 0;
            LastMinuteMoney = 0;
            Settings = settings;

            TimerCallback TimerDelegate1 = new TimerCallback(TakeFineFromBalance);
            Timer fineTimer = new Timer(TimerDelegate1, this, Settings.timeout, Settings.timeout);

            TimerCallback TimerDelegate2 = new TimerCallback(LogLastMinuteMoney);
            Timer logTimer = new Timer(TimerDelegate2, this, 60000, 60000);
        }

        public Response AddCar(string type, string balance)
        {
            try
            {
                if (Settings.totalSpace - CarList.Count() <= 0)
                {
                    return new Response((int)HttpStatusCode.OK, new Dictionary<string, string>() { { "Error", "No enough space!" } });
                }

                if (!int.TryParse(type, out int Type))
                {
                    throw new ArgumentException("Wrong car type!");
                }

                if (!int.TryParse(balance, out int Balance))
                {
                    throw new ArgumentException("Wrong balance!");
                }

                if (!Enum.IsDefined(typeof(Car.CarTypes), Type))
                {
                    return new Response((int)HttpStatusCode.NotFound, new Dictionary<string, string>() { { "Error", "Type isn't in list of types!" } });
                }

                ++LastAddedCarId;

                if (Balance <= 0)
                {
                    Balance = 0;
                }
                else
                {
                    TransactionList.Add(new Transaction(LastAddedCarId, Balance));
                }

                var car = new Car(
                    LastAddedCarId,
                    Balance,
                    (Car.CarTypes)Type
                );
                CarList.Add(car);

                return new Response((int)HttpStatusCode.Created, car);
            }
            catch (ArgumentException ex)
            {
                return new Response((int)HttpStatusCode.Conflict, new Dictionary<string, string>() { { "Error", ex.Message } });
            }
        }

        public Response RemoveCar(string id)
        {
            try
            {
                if (!int.TryParse(id, out int Id))
                {
                    throw new ArgumentException("Wrong car id!");
                }

                var car = CarList.SingleOrDefault(c => c.Id == Id);
                if (car == null)
                {
                    return new Response((int)HttpStatusCode.NotFound, new Dictionary<string, string>() { { "Error", "Car not found!" } });
                }

                if (car.Balance < 0)
                {
                    return new Response((int)HttpStatusCode.OK, new Dictionary<string, string>() { { "Error", "Сar has a negative balance!" } });
                }

                CarList.Remove(car);
                return new Response((int)HttpStatusCode.Created, null);
            }
            catch (ArgumentException ex)
            {
                return new Response((int)HttpStatusCode.BadRequest, new Dictionary<string, string>() { { "Error", ex.Message } });
            }
        }

        public Response GetCarsList()
        {
            return new Response((int)HttpStatusCode.OK, CarList);
        }

        public Response GetCar(string id)
        {
            try
            {
                if (!int.TryParse(id, out int Id))
                {
                    throw new ArgumentException("Wrong arguments!");
                }
                var car = CarList.SingleOrDefault(c => c.Id == Id);
                if (car == null)
                {
                    return new Response((int)HttpStatusCode.NotFound, new Dictionary<string, string>() { { "Error", "Car not found!" } });
                }

                return new Response((int)HttpStatusCode.OK, car);
            }
            catch (ArgumentException ex)
            {
                return new Response((int)HttpStatusCode.BadRequest, new Dictionary<string, string>() { { "Error", ex.Message } });
            }
        }

        public Response RaiseCarBalance(string id, string sum)
        {
            try
            {
                if (!int.TryParse(id, out int Id))
                {
                    throw new ArgumentException("Wrong car id!");
                }

                if (!int.TryParse(sum, out int Sum))
                {
                    throw new ArgumentException("Wrong sum count!");
                }

                var car = CarList.SingleOrDefault(c => c.Id == Id);
                if (car == null)
                {
                    return new Response((int)HttpStatusCode.NotFound, new Dictionary<string, string>() { { "Error", "Car not found!" } });
                }

                car.ChangeBalance(Sum);
                TransactionList.Add(new Transaction(Id, Sum));
                return new Response((int)HttpStatusCode.OK, car);
            }
            catch (ArgumentException ex)
            {
                return new Response((int)HttpStatusCode.BadRequest, new Dictionary<string, string>() { { "Error", ex.Message } });
            }
        }

        public void TakeFineFromBalance(object StateObj)
        {
            Parking State = (Parking)StateObj;
            var sum = 0;
            int price = 0;
            foreach (var car in State.CarList)
            {
                price = Settings.prices[car.CarType];
                sum = (car.Balance >= 0) ? price * (-1) : price * Settings.fine * (-1);

                car.ChangeBalance(sum);
                State.TransactionList.Add(new Transaction(car.Id, sum * (-1)));
                State.EarnedMoney += sum;
                State.LastMinuteMoney += sum;
            }
        }

        public Response GetFreePlacesCount()
        {
            return new Response((int)HttpStatusCode.OK, Settings.totalSpace - CarList.Count());
        }

        public Response UsingPlacesCount()
        {
            return new Response((int)HttpStatusCode.OK, CarList.Count());
        }

        public Response GetEarnedMoney()
        {
            return new Response((int)HttpStatusCode.OK, EarnedMoney);
        }

        public Response GetLastMinuteTransactions()
        {
            List<Transaction> list = (from transaction in TransactionList
                                      where transaction.DateTime.AddMinutes(1).Minute >= DateTime.Now.Minute
                                      select transaction)
                                      .ToList();

            return new Response((int)HttpStatusCode.OK, list);
        }

        public Response GetLastMinuteTransactions(string id)
        {
            try
            {
                if (!int.TryParse(id, out int Id))
                {
                    throw new ArgumentException("Wrong arguments!");
                }
                List<Transaction> list = (from transaction in TransactionList
                                          where transaction.DateTime.AddMinutes(1).Minute >= DateTime.Now.Minute
                                          where transaction.CarId == Id
                                          select transaction)
                                          .ToList();

                return new Response((int)HttpStatusCode.OK, list);
            }
            catch (ArgumentException ex)
            {
                return new Response((int)HttpStatusCode.BadRequest, new Dictionary<string, string>() { { "Error", ex.Message } });
            }
        }

        public void LogLastMinuteMoney(object StateObj)
        {
            try
            {
                Parking State = (Parking)StateObj;
                string log = DateTime.Now.ToString("MM.dd.yyyy HH:mm +") + State.LastMinuteMoney;
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
                Console.WriteLine(ex.Message);
            }
        }

        public Response ShowAllTransactions()
        {
            try
            {
                var list = new List<Dictionary<string, string>>();
                string line = String.Empty;
                string[] token = new string[3];
                using (StreamReader sr = new StreamReader(Settings.filePath, Encoding.Default))
                {
                    do
                    {
                        line = sr.ReadLine();
                        if (line != String.Empty)
                        {
                            token = line.Split(" ");
                            list.Add(new Dictionary<string, string>()
                            {
                                { "Date", token[0] },
                                { "Time", token[1] },
                                { "Cash", token[2] },
                            });
                        }
                    } while (!sr.EndOfStream);
                }
                return new Response((int)HttpStatusCode.OK, list);
            }
            catch (FileNotFoundException)
            {
                return new Response((int)HttpStatusCode.InternalServerError, new Dictionary<string, string>() { { "Error", "File not found!" } });
            }
            catch (FileLoadException)
            {
                return new Response((int)HttpStatusCode.InternalServerError, new Dictionary<string, string>() { { "Error", "Error while load file!" } });
            }
            catch (IOException ex)
            {
                return new Response((int)HttpStatusCode.InternalServerError, new Dictionary<string, string>() { { "Error", ex.Message } });
            }
        }
    }
}
