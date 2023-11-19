using Console_dbApp.Models.Car;
using System.Diagnostics;

namespace Console_dbApp.Models.Entities;

public class CarEntity
{
    public int Id { get; set; }
    public string Model { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public int OrderId { get; set; }
    public ICollection<OrderEntity> Order { get; set; } = null!;

    public int EngineId { get; set; }
    public EngineEntity Engine { get; set; } = null!;

    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;

    public int ManufacturerId { get; set; }
    public ManufacturerEntity Manufacturer { get; set; } = null!;

    public int ColorId { get; set; }
    public ColorEntity Color { get; set; } = null!;

    public int ModelYearId { get; set; }
    public ModelYearEntity ModelYear { get; set; } = null!;


    public static implicit operator CarEntity(CarRegistration registration)
    {
        try
        {
            return new CarEntity
            {
                Model = registration.Model,
                Description = registration.Description,
                Engine = new EngineEntity
                {
                    Type = registration.EngineType,
                    Description = registration.EngineDescription,
                },
                Category = new CategoryEntity
                {
                    CategoryName = registration.CategoryName
                },
                Manufacturer = new ManufacturerEntity
                {
                    Name = registration.ManufacturerName,
                    Country = registration.ManufacturerCountry,
                },
                Color = new ColorEntity
                {
                    Color = registration.Color
                },
                ModelYear = new ModelYearEntity
                {
                    Year = registration.Year
                },
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}

