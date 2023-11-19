namespace Console_dbApp.Models.Entities;

public class ModelYearEntity
{
    public int Id { get; set; }
    public string Year { get; set; } = null!;

    public int CarId { get; set; }
    public ICollection<CarEntity> Car { get; set; } = null!;
}

