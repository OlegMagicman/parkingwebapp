# Parking management web application
It is a simple parking management application on ASP.NET Core which provide WEB API to communicate with server.  
At this time program can add or remove car from parking, raise car balance and get list of cars.  
Every N-seconds (default 3) it takes fine from car balance in according to car type. If car have negative balance fine multiplying on coefficient.  
Every minute transaction sum logging into file.  
All program settings described in Settings class.

## How to run  
```
dotnet build  
dotnet run  
```

## API Reference  
### Cars  

| Method    | HTTP Request              | Description             |
| --------- | ------------------------- | ----------------------- |
| GetCars   | GET /api/Cars             | Return list of cars     |
| GetCar    | GET /api/Cars/{id}        | Return info about car   |
| RemoveCar | DELETE /api/Cars/{id}     | Remove car from parking |
| AddCar    | POST /api/Cars/{cartype}  | Add car to parking      |

### Parking  

| Method         | HTTP Request            | Description                    |
| -------------- | ----------------------- | ------------------------------ |
| GetFreePlaces  | GET /api/Parking/free   | Return amount of free places   |
| GetUsingPlaces | GET /api/Parking/using  | Return amount of using places  |
| GetEarnedMoney | GET /api/Parking/earned | Remove summary of earned money |

### Transaction  

| Method                    | HTTP Request                                        | Description                         |
| ------------------------- | --------------------------------------------------- | ----------------------------------- |
| GetTransactions           | GET /api/Transaction/GetTransactions                | Transaction.log data                |
| GetLastMinuteTransactions | GET /api/Transaction/GetLastMinuteTransactions      | Transactions of last minute         |
| GetLastMinuteTransactions | GET /api/Transaction/GetLastMinuteTransactions/{id} | Transactions of last minute for car |
| Balance                   | PUT /api/Transaction/Balance/{id}/{sum}             | Raise car balance                   |
