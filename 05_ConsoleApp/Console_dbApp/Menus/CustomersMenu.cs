using System.Diagnostics;
using Console_dbApp.Models.Customer;
using Console_dbApp.Models.Entities;
using Console_dbApp.Services;

namespace Console_dbApp.Menus;

public class CustomersMenu
{
    private readonly CustomerService _customerService;

    public CustomersMenu(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ShowAsync()
    {
        var exit = false;

        do
        {
            Console.Clear();
            Console.WriteLine("[1] Visa alla kunder");
            Console.WriteLine("[2] Lägg till ny kund");
            Console.WriteLine("[3] Uppdatera en kund");
            Console.WriteLine("[4] Ta bort en kund");
            Console.WriteLine("[5] Inaktivera en kund");
            Console.WriteLine("[6] Återaktivera en inaktiv kund");
            Console.WriteLine("[0] <-- Gå bakåt");

            Console.Write("Välj ett alternativ: ");
            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    await ShowAllCustomers();
                    break;

                case "2":
                    await ShowCreateCustomer();
                    break;

                case "3":
                    await UpdateCustomer();
                    break;

                case "4":
                    await RemoveCustomer();
                    break;

                case "5":
                    await InactivateCustomer();
                    break;

                case "6":
                    await ActivateCustomer();
                    break;

                case "0":
                    exit = true;
                    break;
            }
        }
        while (!exit);
    }

    public async Task UpdateCustomer()
    {
        await ShowAllCustomers();

        Console.Write("Välj ett [kundId] som du vill uppdatera: ");
        try
        {
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                await ShowCustomer(customerId);

                var customerUpdate = new CustomerRegistration();
                Console.WriteLine("ANGE NYA UPPGIFTER FÖR KUNDEN");
                Console.WriteLine("OBS! Vill du inte ändra en speicfik uppgift, tryck bara enter!");
                Console.Write("Förnamn: ");
                customerUpdate.FirstName = Console.ReadLine()!;
                Console.Write("Efternamn: ");
                customerUpdate.LastName = Console.ReadLine()!;

                Console.Write("Mejladress: ");
                customerUpdate.Email = Console.ReadLine()!;
                Console.Write("Telefonnummer: ");
                customerUpdate.PhoneNumber = Console.ReadLine()!;

                Console.Write("Gatunamn: ");
                customerUpdate.StreetName = Console.ReadLine()!;
                Console.Write("Gatunummer (valfritt): ");
                customerUpdate.StreetNumber = Console.ReadLine();
                Console.Write("Postnummer: ");
                customerUpdate.ZipCode = Console.ReadLine()!;
                Console.Write("Stad / Ort: ");
                customerUpdate.City = Console.ReadLine()!;

                await _customerService.UpdateCustomerAsync(customerId, customerUpdate);
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task ShowCustomer(int customerId)
    {
        try
        {
            var customer = await _customerService.GetCustomerAsync(customerId);

            Console.Clear();
            Console.WriteLine("KUNDENS UPPGIFTER");
            Console.WriteLine($"Förnamn: {customer.FirstName}");
            Console.WriteLine($"Efternamn: {customer.LastName}");
            Console.WriteLine($"Mejladress: {customer.ContactInformation.Email}");
            Console.WriteLine($"Mejladress: {customer.ContactInformation.PhoneNumber}");
            Console.WriteLine($"Gatunamn: {customer.Address.StreetName}");
            Console.WriteLine($"Gatunummer: {customer.Address.StreetNumber}");
            Console.WriteLine($"Postnummer: {customer.Address.ZipCode}");
            Console.WriteLine($"Stad / Ort: {customer.Address.City}");
            Console.WriteLine();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
    }

    public async Task ShowCreateCustomer()
    {
        try
        {
            var customerRegistration = new CustomerRegistration();

            Console.Clear();
            Console.WriteLine("--- LÄGG TILL NY KUND ---");
            Console.Write("Förnamn: ");
            customerRegistration.FirstName = Console.ReadLine()!;
            Console.Write("Efternamn: ");
            customerRegistration.LastName = Console.ReadLine()!;

            Console.Write("Mejladress: ");
            customerRegistration.Email = Console.ReadLine()!;
            Console.Write("Telefonnummer: ");
            customerRegistration.PhoneNumber = Console.ReadLine()!;

            Console.Write("Gatunamn: ");
            customerRegistration.StreetName = Console.ReadLine()!;
            Console.Write("Gatunummer (valfritt): ");
            customerRegistration.StreetNumber = Console.ReadLine();
            Console.Write("Postnummer: ");
            customerRegistration.ZipCode = Console.ReadLine()!;
            Console.Write("Stad / Ort: ");
            customerRegistration.City = Console.ReadLine()!;

            var result = await _customerService.CreateCustomerAsync(customerRegistration);
            var (customerCreated, message) = result;

            if (customerCreated != null)
            {
                Console.Clear();
                Console.WriteLine($"{message}");
                Console.WriteLine($"{customerCreated.FullName}");
                Console.WriteLine($"{customerCreated.ContactInformation.FullContactInfo}");
                Console.WriteLine($"{customerCreated.Address.FullAddress}");
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

    public async Task<bool> ShowAllCustomers1()
    {
        try
        {
            Console.Clear();
            var customers = await _customerService.GetAllAsync();
            if (customers != null && customers.Any())
            {
                Console.WriteLine("--- KUNDLISTA ---");
                foreach (var customer in customers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

                return true;
            }
            else
                Console.WriteLine("Kundlistan är tom..");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task ShowAllCustomers()
    {
        try
        {
            Console.Clear();
            var activeCustomers = await _customerService.GetAllAsync(true);
            var inactiveCustomers = await _customerService.GetAllAsync(false);
            if (activeCustomers.Any() || inactiveCustomers.Any())
            {
                Console.WriteLine("--- AKTIVA KUNDER ---");
                foreach (var customer in activeCustomers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

                Console.WriteLine();
                Console.WriteLine("--- INAKTIVA KUNDER ---");
                foreach (var customer in inactiveCustomers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

            }
            else
                Console.WriteLine("Kundlistan är tom..");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        Console.ReadKey();
    }
    
    public async Task RemoveCustomer()
    {
        try
        {
            Console.Clear();
            var customers = await _customerService.GetAllAsync(true);
            if (customers != null && customers.Any())
            {
                Console.WriteLine("--- AKTIVA KUNDER ---");
                foreach (var customer in customers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

                Console.Write("Välj ett [kundId] som du vill ta bort: ");
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    var result = await _customerService.RemoveCustomerAsync(customerId);

                    if (result)
                        Console.WriteLine("Raderingen lyckades!");
                    else
                        Console.WriteLine("Något gick fel, raderingen misslyckades..");
                }
                else
                    Console.WriteLine("Felaktigt Id angivet");
            }
            else
                Console.WriteLine("Kundlistan är tom..");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        Console.ReadKey();
    }
    public async Task InactivateCustomer()
    {
        try
        {
            Console.Clear();
            var customers = await _customerService.GetAllAsync(true);
            if (customers != null && customers.Any())
            {
                Console.WriteLine("--- AKTIVA KUNDER ---");
                foreach (var customer in customers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

                Console.Write("Välj ett [kundId] som du vill inaktivera: ");
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    var result =await _customerService.ChangeIsActiveAsync(customerId);

                    if (result)
                        Console.WriteLine("Inaktiveringen lyckades!");
                    else
                        Console.WriteLine("Något gick fel, inaktiveringen misslyckades..");
                }
                else
                    Console.WriteLine("Felaktigt Id angivet");
            }
            else
                Console.WriteLine("Kundlistan är tom..");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

            Console.ReadKey();
    }

    public async Task ActivateCustomer()
    {
        try
        {
            Console.Clear();
            var customers = await _customerService.GetAllAsync(false);
            if (customers != null && customers.Any())
            {
                Console.WriteLine("--- INAKTIVA KUNDER ---");
                foreach (var customer in customers)
                    Console.WriteLine($"[{customer.Id}] {customer.FullName} -- {customer.ContactInformation.FullContactInfo} -- {customer.Address.FullAddress}");

                Console.Write("Välj ett [kundId] som du vill aktivera: ");
                if (int.TryParse(Console.ReadLine(), out int customerId))
                {
                    var result = await _customerService.ChangeIsActiveAsync(customerId);

                    if (result)
                        Console.WriteLine("Aktiveringen lyckades!");
                    else
                        Console.WriteLine("Något gick fel, aktiveringen misslyckades..");
                }
                else
                    Console.WriteLine("Felaktigt Id angivet");
            }
            else
                Console.WriteLine("Kundlistan är tom..");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

            Console.ReadKey();
    }

}

