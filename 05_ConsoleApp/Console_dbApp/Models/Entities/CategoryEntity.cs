namespace Console_dbApp.Models.Entities;

public class CategoryEntity
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;

    public int CarId { get; set; }
    public ICollection<CarEntity> Car { get; set; } = null!;
}

