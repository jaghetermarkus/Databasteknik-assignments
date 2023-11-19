namespace Console_dbApp.Models.Entities;

public class ManufacturerEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;

    public int CarId { get; set; }
    public ICollection<CarEntity> Car { get; set; } = null!;
}

