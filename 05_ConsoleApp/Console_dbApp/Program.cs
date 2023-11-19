using Console_dbApp.Contexts;
using Console_dbApp.Menus;
using Console_dbApp.Repositories;
using Console_dbApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Console_dbApp;

public class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=tcp:markus-karlsson-cms23.database.windows.net,1433;Initial Catalog=AZURE_DB;Persist Security Info=False;User ID=sqladmin;Password=$^$Vu$Y5Mz2mq@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

        services.AddScoped<MainMenu>();
        services.AddScoped<CustomersMenu>();
        services.AddScoped<OrdersMenu>();
        services.AddScoped<CarsMenu>();

        services.AddScoped<CustomerRepository>();
        services.AddScoped<AddressRepository>();
        services.AddScoped<ContactInformationRepository>();
        services.AddScoped<OrderRepository>();
        services.AddScoped<CarRepository>();
        services.AddScoped<ColorRepository>();
        services.AddScoped<EngineRepository>();
        services.AddScoped<CategoryRepository>();
        services.AddScoped<ManufacturerRepository>();
        services.AddScoped<ModelYearRepository>();

        services.AddScoped<CustomerService>();
        services.AddScoped<CarService>();
        services.AddScoped<OrderService>();

        using var sp = services.BuildServiceProvider();
        var mainMenu = sp.GetRequiredService<MainMenu>();
        await mainMenu.ShowAsync();

        // var customerTypeService = sp.GetRequiredService<CustomerTypeService>();
        // await customerTypeService!.CreateCustomerTypeMenuAsync();
    }
}

