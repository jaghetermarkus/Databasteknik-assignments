using Console_dbApp.Models.Car;
using Console_dbApp.Services;
using System.Diagnostics;

namespace Console_dbApp.Menus;

public class CarsMenu
{
    private readonly CarService _carService;

    public CarsMenu(CarService carService)
    {
        _carService = carService;
    }

    
        public async Task ShowAsync()
        {
            var exit = false;

            do
            {
                Console.Clear();
                Console.WriteLine("[1] Visa alla bilar");
                Console.WriteLine("[2] Lägg till ny bil");
                Console.WriteLine("[3] Inaktivera en bil");
                Console.WriteLine("[4] Återaktivera en bil"); 
                Console.WriteLine("[0] <-- Gå bakåt");

                Console.Write("Välj ett alternativ: ");
                var userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        await ShowAllCars();
                    break;

                    case "2":
                        await ShowCreateCar();
                        break;

                    case "3":
                        await InactivateCar();
                        break;

                    case "4":
                        await ActivateCar();
                        break;

                    case "0":
                        exit = true;
                        break;
                }
            }
            while (!exit);
        }
     

        public async Task ShowCreateCar()
        {
            try
            {
                var carRegistration = new CarRegistration();

                Console.Clear();
                Console.WriteLine("--- LÄGG TILL NY BIL ---");

                Console.Write("Bilmärke: ");
                carRegistration.ManufacturerName = Console.ReadLine()!;
                Console.Write("Land: ");
                carRegistration.ManufacturerCountry = Console.ReadLine()!;
                Console.Write("Model: ");
                carRegistration.Model = Console.ReadLine()!;
                Console.Write("Eventuell modelbeskrivning: ");
                carRegistration.Description = Console.ReadLine();
                Console.Write("Årsmodel: ");
                carRegistration.Year = Console.ReadLine()!;

                Console.Write("Typ av bil (SUV, Cab, Kombi osv..): ");
                carRegistration.CategoryName = Console.ReadLine()!;

                Console.Write("Motortype: ");
                carRegistration.EngineType = Console.ReadLine()!;
                Console.Write("Eventuell motorbeskrivning: ");
                carRegistration.EngineDescription = Console.ReadLine();


                Console.Write("Färg: ");
                carRegistration.Color = Console.ReadLine()!;

                var result = await _carService.CreateCarAsync(carRegistration);
                var (carCreated, message) = result;

                if (carCreated != null)
                {
                    Console.Clear();
                    Console.WriteLine($"{message}");
                    Console.WriteLine($"{carCreated.Manufacturer.Name} {carCreated.Model} {carCreated.Description} {carCreated.ModelYear.Year} {carCreated.Color.Color}");
                    Console.WriteLine($"{carCreated.Category.CategoryName} {carCreated.Engine.Type}");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"{message}");
                    Console.ReadKey();
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

        }

        public async Task ShowAllCars()
        {
            try
            {
                Console.Clear();
                var activeCars = await _carService.GetAllAsync(true);
                var inactiveCars = await _carService.GetAllAsync(false);
                if (activeCars.Any() || inactiveCars.Any())
                {
                    
                    Console.WriteLine("--- AKTIVA BILAR ---");
                    foreach (var car in activeCars)
                        Console.WriteLine($"[{car.Id}] {car.Manufacturer.Name} {car.Model} {car.ModelYear.Year}, {car.Engine.Type} {car.Color.Color}");

                    Console.WriteLine();
                    Console.WriteLine("--- INAKTIVA BILAR ---");
                    foreach (var car in inactiveCars)
                        Console.WriteLine($"[{car.Id}] {car.Manufacturer.Name} {car.Model} {car.ModelYear.Year}, {car.Engine.Type} {car.Color.Color}");
                }
                else
                {
                    Console.WriteLine("Listan med bilar är tom..");
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

        Console.ReadKey();
        }

        public async Task ShowAllCars(bool isActive)
        {
            try
            {
                Console.Clear();
                var cars = await _carService.GetAllAsync(isActive);
                if (cars != null)
                {
                    Console.WriteLine("--- BILAR ---");
                    foreach (var car in cars)
                        Console.WriteLine($"[{car.Id}] {car.Manufacturer.Name} {car.Model} {car.ModelYear.Year}, {car.Engine.Type} {car.Color.Color}");
                }
                else
                {
                    Console.WriteLine("Listan med bilar är tom..");
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

    public async Task InactivateCar()
        {
            try
            {
                Console.Clear();
                var cars = await _carService.GetAllAsync(true);
                if (cars != null && cars.Any())
                {
                    Console.WriteLine("--- AKTIVA BILAR ---");
                    foreach (var car in cars)
                        Console.WriteLine($"[{car.Id}] {car.Manufacturer.Name} {car.Model} {car.ModelYear.Year}, {car.Engine.Type} {car.Color.Color}");

                    Console.Write("Välj ett [Id] som du vill inaktivera: ");
                    if (int.TryParse(Console.ReadLine(), out int carId))
                    {
                        var result = await _carService.ChangeIsActiveAsync(carId);

                        if (result)
                            Console.WriteLine("Inaktiveringen lyckades!");
                        else
                            Console.WriteLine("Något gick fel, inaktiveringen misslyckades..");
                    }
                    else
                        Console.WriteLine("Felaktigt Id angivet");
                }
                else
                    Console.WriteLine("Billistan är tom..");

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            Console.ReadKey();
        }

        public async Task ActivateCar()
        {
            try
            {
                Console.Clear();
                var cars = await _carService.GetAllAsync(false);
                if (cars != null && cars.Any())
                {
                    Console.WriteLine("--- INAKTIVA BILAR ---");
                    foreach (var car in cars)
                        Console.WriteLine($"[{car.Id}] {car.Manufacturer.Name} {car.Model} {car.ModelYear.Year}, {car.Engine.Type} {car.Color.Color}");

                    Console.Write("Välj ett [Id] som du vill aktivera: ");
                    if (int.TryParse(Console.ReadLine(), out int carId))
                    {
                        var result = await _carService.ChangeIsActiveAsync(carId);

                        if (result)
                            Console.WriteLine("Aktiveringen lyckades!");
                        else
                            Console.WriteLine("Något gick fel, aktiveringen misslyckades..");
                    }
                    else
                        Console.WriteLine("Felaktigt Id angivet");
                }
                else
                    Console.WriteLine("Billistan är tom..");

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            Console.ReadKey();
        }
}

