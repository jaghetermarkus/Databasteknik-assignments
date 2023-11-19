namespace Console_dbApp.Menus;

public class MainMenu
{
    private readonly CustomersMenu _customersMenu;
    private readonly OrdersMenu _ordersMenu;
    private readonly CarsMenu _carsMenu;

    public MainMenu(CustomersMenu customersMenu, OrdersMenu ordersMenu, CarsMenu carsMenu)
    {
        _customersMenu = customersMenu;
        _ordersMenu = ordersMenu;
        _carsMenu = carsMenu;
    }

    public async Task ShowAsync()
    {
        var exit = false;

        do
        {
            Console.Clear();
            Console.WriteLine("[1] Gå till Kunder");
            Console.WriteLine("[2] Gå till Bilar");
            Console.WriteLine("[3] Gå till Ordrar");
            Console.WriteLine("[0] Avsluta");
            Console.Write("Välj ett alternativ: ");
            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    await _customersMenu.ShowAsync();
                    break;
                case "2":
                    await _carsMenu.ShowAsync();
                    break;
                case "3":
                    await _ordersMenu.ShowAsync();
                    break;
                case "0":
                    exit = true;
                    break;
            }
        }
        while (!exit);
    }

}

