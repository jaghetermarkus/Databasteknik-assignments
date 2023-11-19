using Console_dbApp.Models.Entities;

namespace Console_dbApp.Models.Car;

public class CarRegistration
{
    public string Model { get; set; } = null!;
    public string? Description { get; set; }

    public string EngineType { get; set; } = null!;
    public string? EngineDescription { get; set; }

    public string CategoryName { get; set; } = null!;

    public string ManufacturerName { get; set; } = null!;
    public string ManufacturerCountry { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Year { get; set; } = null!;



}

