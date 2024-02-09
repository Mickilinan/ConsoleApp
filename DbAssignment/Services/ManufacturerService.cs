using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class ManufacturerService(ManufacturerRepository manufacturerRepository, ProductService productService)
{

    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly ProductService _productService = productService;

    public ManufacturerEntity CreateManufacturer(string productName, string manufacturerName, decimal price, string description, string categoryName)
    {

        var productEntity = _productService.CreateProduct(productName, price, description, categoryName);
        if (productEntity == null)
        {
            return null;
        }

        var manufacturerEntity = new ManufacturerEntity

        {
            ManufacturerName = manufacturerName,          
            ProductId = productEntity.Id
        };

        manufacturerEntity = _manufacturerRepository.Create(manufacturerEntity);
        return manufacturerEntity;
    }

    public ManufacturerEntity GetManufacturerById(int id)
    {
        var manufacturerEntity = _manufacturerRepository.Get(x => x.Id == id);
        return manufacturerEntity;
    }

    public IEnumerable<ManufacturerEntity> GetAllManufacturers()
    {
        var manufacturers = _manufacturerRepository.GetAll();
        return manufacturers;
    }

    public ManufacturerEntity UpdateManufacturer(ManufacturerEntity manufacturerEntity)
    {
        var updatedManufacturer = _manufacturerRepository.Update(x => x.Id == manufacturerEntity.Id, manufacturerEntity);
        return updatedManufacturer;
    }


    public void DeleteManufacturer(int Id)
    {
        _manufacturerRepository.Delete(x => x.Id == Id);

    }
}
