using System.Diagnostics;
using Console_dbApp.Models.Car;
using Console_dbApp.Models.Customer;
using Console_dbApp.Models.Entities;
using Console_dbApp.Repositories;

namespace Console_dbApp.Services;

public class CarService
{
    private readonly CarRepository _carRepo;
    private readonly EngineRepository _engineRepo;
    private readonly CategoryRepository _categoryRepo;
    private readonly ManufacturerRepository _manufacturerRepo;
    private readonly ColorRepository _colorRepo;
    private readonly ModelYearRepository _modelYearRepo;

    public CarService(CarRepository carRepo, EngineRepository engineRepo, CategoryRepository categoryRepo, ManufacturerRepository manufacturerRepo, ColorRepository colorRepo, ModelYearRepository modelYearRepo)
    {
        _carRepo = carRepo;
        _engineRepo = engineRepo;
        _categoryRepo = categoryRepo;
        _manufacturerRepo = manufacturerRepo;
        _colorRepo = colorRepo;
        _modelYearRepo = modelYearRepo;
    }

    // CREATE car, receive properties from CarRegistration
    public async Task<(CarEntity?, string)> CreateCarAsync(CarRegistration registration)
    {
        try
        {
            CarEntity carEntity = registration;

            if (!await _carRepo.ExistsAsync(x =>
                x.Model == carEntity.Model && 
                x.Category.CategoryName == carEntity.Category.CategoryName &&
                x.Engine.Type == carEntity.Engine.Type &&
                x.Color.Color == carEntity.Color.Color &&
                x.ModelYear.Year == carEntity.ModelYear.Year
                ))
            {
                // See if propteries already exists in the database, then reuse that to the new carEntity
                var engineEntity = await _engineRepo.ReadAsync(x => x.Type == carEntity.Engine.Type);
                if (engineEntity != null)
                    carEntity.Engine = engineEntity;

                var categoryEntity = await _categoryRepo.ReadAsync(x => x.CategoryName == carEntity.Category.CategoryName);
                if (categoryEntity != null)
                    carEntity.Category = categoryEntity;

                var manufacturerEntity = await _manufacturerRepo.ReadAsync(x => x.Name == carEntity.Manufacturer.Name);
                if (manufacturerEntity != null)
                    carEntity.Manufacturer = manufacturerEntity;

                var colorEntity = await _colorRepo.ReadAsync(x => x.Color == carEntity.Color.Color);
                if (colorEntity != null)
                    carEntity.Color = colorEntity;

                var modelYearEntity = await _modelYearRepo.ReadAsync(x => x.Year == carEntity.ModelYear.Year);
                if (modelYearEntity != null)
                    carEntity.ModelYear = modelYearEntity;

                // Create new carEntity
                CarEntity carCreated = await _carRepo.CreateAsync(carEntity);

                return (carCreated, "NY BIL HAR LAGTS TILL:");
            }
            else
            {
                return (null, "Bilmodellen du angav finns redan, bilen skapades inte.");
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return (null, "Ett fel uppstod, kunden skapades inte..");
    }

    // GET ALL CARS
    public async Task<IEnumerable<CarEntity>> GetAllAsync()
    {
        var cars = await _carRepo.ReadAsync();

        return cars.ToList();
    }

    // GET ALL Cars absed on the 'IsActive' state
    public async Task<IEnumerable<CarEntity>> GetAllAsync(bool input)
    {
        try
        {
            var cars = await _carRepo.ReadAsync(isActive: input);
            return cars.ToList();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // GET ONE car based on provided carId
    public async Task<CarEntity> GetCarAsync(int carId)
    {
        try
        {
            return await _carRepo.ReadAsync(carId);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;
    }

    // SOFT-DELETE - Changes the 'IsActive' state of a car
    public async Task<bool> ChangeIsActiveAsync(int carId)
    {
        try
        {
            return await _carRepo.ChangeActiveStateAsync(x => x.Id == carId);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false!;
    }

}

