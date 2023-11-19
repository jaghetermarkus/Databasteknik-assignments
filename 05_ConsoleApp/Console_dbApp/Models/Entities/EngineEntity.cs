namespace Console_dbApp.Models.Entities;

public class EngineEntity
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    public string? Description { get; set; }

    public int CarId { get; set; }
    public ICollection<CarEntity> Car { get; set; } = null!;
}

