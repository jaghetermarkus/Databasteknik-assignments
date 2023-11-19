using Console_dbApp.Models.Customer;
using Console_dbApp.Models.Order;
using System.Diagnostics;

namespace Console_dbApp.Models.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public ICollection<CustomerOrderEntity> CustomerOrders { get; set; } = new List<CustomerOrderEntity>();

    public int CarId { get; set; }
    public CarEntity Car { get; set; } = null!;

    public static implicit operator OrderEntity(OrderRegistration registration)
    {
        try
        {
            return new OrderEntity
            {
                CarId = registration.CarId,
                CustomerOrders = new List<CustomerOrderEntity>
                {
                    new CustomerOrderEntity
                    {
                        CustomerId = registration.CustomerId,
                    }
                }
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}

