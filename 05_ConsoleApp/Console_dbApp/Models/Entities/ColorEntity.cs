namespace Console_dbApp.Models.Entities;

public class ColorEntity
{
    public int Id { get; set; }
    public string Color { get; set; } = null!;

    public int CarId { get; set; }
    public ICollection<CarEntity> Car { get; set; } = null!;
}

