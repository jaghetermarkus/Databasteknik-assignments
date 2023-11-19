using System.Diagnostics;
using Console_dbApp.Services;

namespace Console_dbApp.Menus;

public class OrdersMenu
{
    private readonly CustomerService _customerService;
    private readonly CarService _carService;
    private readonly OrderService _orderService;
    private readonly CustomersMenu _customerMenu;
    private readonly CarsMenu _carMenu;

    public OrdersMenu(CustomerService customerService, CarService carService, CustomersMenu customerMenu, CarsMenu carMenu, OrderService orderService)
    {
        _customerService = customerService;
        _carService = carService;
        _customerMenu = customerMenu;
        _carMenu = carMenu;
        _orderService = orderService;
    }


    public async Task ShowAsync()
    {
       var exit = false;

       do
       {
           Console.Clear();
           Console.WriteLine("[1] Visa alla ordrar");
           Console.WriteLine("[2] Skapa ny order");
           Console.WriteLine("[0] <-- Gå bakåt");

           Console.Write("Välj ett alternativ: ");
           var userInput = Console.ReadLine();

           switch (userInput)
           {
               case "1":
                    await ShowAllOrders();
                   break;

               case "2":
                    await ShowCreateOrder();
                   break;

               case "0":
                   exit = true;
                   break;
           }
       }
       while (!exit);
    }


    public async Task ShowCreateOrder()
    {
        try
        {
            Console.Clear();
            // Retrive all active customer and cars 
            var customers = await _customerService.GetAllAsync(true);
            var cars = await _carService.GetAllAsync(true);

            // See if both customer/car list has any objects
            if (customers.Any() && cars.Any())
            {
                Console.WriteLine("--- AKTIVA KUNDER ---");
                foreach (var customer in customers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

                Console.Write("Välj ett [kundId] som du vill skapa en order för: ");
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.Clear();
                    var customerEntity = await _customerService.GetCustomerAsync(customerId);
                    Console.WriteLine($"Du valde kund: {customerEntity.FullName} {customerEntity.ContactInformation.FullContactInfo}");
                };

                Console.WriteLine("--- AKTIVA BILAR ---");
                foreach (var car in cars)
                    Console.WriteLine($"[{car.Id}] {car.Manufacturer.Name} {car.Model} {car.ModelYear.Year}, {car.Engine.Type} {car.Color.Color}");

                Console.Write("Välj ett [bilId] som du vill skapa en order för: ");
                if (int.TryParse(Console.ReadLine(), out int carId))
                {
                    var carEntity = await _carService.GetCarAsync(carId);
                    Console.WriteLine($"Du valde bil: {carEntity.Manufacturer.Name} {carEntity.Model} {carEntity.Category.CategoryName} {carEntity.ModelYear.Year} {carEntity.Color.Color}");
                };

                // Send carId and CustomerId to create new order
                var result = await _orderService.CreateOrderAsync(carId, customerId);
                if (result)
                {
                    Console.WriteLine("Din order skapades");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Något gick fel, ordern skapades inte");
                }

            }
            else
            {
                Console.WriteLine("Kundlistan och/eller billistan är tom, ingen order kan skapas.");
            }

        }
        catch (Exception ex) { }

        Console.ReadKey();
    }


    public async Task ShowAllOrders()
    {
        try
        {
            Console.Clear();
            var orders = await _orderService.GetAllAsync();
            if (orders != null)
            {
                Console.WriteLine("--- ORDERLISTA ---");
                foreach (var order in orders)
                {
                    Console.Write($"[{order.Id}] {order.OrderDate} -- ");
                    foreach (var customerOrder in order.CustomerOrders)
                    {
                        Console.Write($"{customerOrder.Customer?.FullName} ");
                    }
                    Console.WriteLine($"-- {order.Car.Manufacturer.Name} {order.Car.Model} {order.Car.ModelYear.Year} {order.Car.Category.CategoryName} {order.Car.Color.Color}");
                }
            }
            else
            {
                Console.WriteLine("Orderlistan är tom..");
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        Console.ReadKey();
    }
}
