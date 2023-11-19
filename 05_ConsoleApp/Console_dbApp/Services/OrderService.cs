using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Console_dbApp.Models.Car;
using Console_dbApp.Models.Customer;
using Console_dbApp.Models.Entities;
using Console_dbApp.Models.Order;
using Console_dbApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Console_dbApp.Services;

public class OrderService
{
    private readonly CarRepository _carRepo;
    private readonly CustomerRepository _cusomterRepo;
    private readonly OrderRepository _orderRepo;

    public OrderService(CarRepository carRepo, CustomerRepository cusomterRepo, OrderRepository orderRepo)
    {
        _carRepo = carRepo;
        _cusomterRepo = cusomterRepo;
        _orderRepo = orderRepo;
    }

    // CREATE new order based on received Car/CustomerId's
    public async Task<bool> CreateOrderAsync(int carId, int customerId)
    {
        try
        {
            // Check that both CarEntity and CustomerEntity exist
            var carEntity = await _carRepo.ReadAsync(carId);
            var customerEntity = await _cusomterRepo.ReadAsync(customerId);

            if (carEntity == null || customerEntity == null)
            {
                // One or both entities are missing, cannot create an order
                return false;
            }

            // Create a new OrderRegistration
            var orderRegistration = new OrderRegistration
            {
                //Set CustomerId/CarId based on the provided Id's
                CustomerId = customerId, 
                CarId = carId   
            };

            // Save the OrderEntity in the database
            await _orderRepo.CreateAsync(orderRegistration);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    // GET all orders
    public async Task<IEnumerable<OrderEntity>> GetAllAsync()
    {
        var orders = await _orderRepo.ReadAsync();

        return orders.ToList();
    }

}

