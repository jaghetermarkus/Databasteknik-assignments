namespace Console_dbApp.Models.Entities;

public class CustomerOrderEntity
{
    public int CustomerId { get; set; }
    public CustomerEntity? Customer { get; set; }

    public int OrderId { get; set; }
    public OrderEntity? Order { get; set; } = null!;
}


