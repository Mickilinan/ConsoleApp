using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class SupplierService
{

    private readonly SupplierRepository _supplierRepository;

    public SupplierService(SupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public SupplierEntity CreateSupplier(string supplierName, string contactInfo)
    {

        var supplierEntity = new SupplierEntity

        {
            SupplierName = supplierName,
            ContactInfo = contactInfo
        };

        supplierEntity = _supplierRepository.Create(supplierEntity);
        return supplierEntity;
    }

    public SupplierEntity GetSupplierById(int id)
    {
        var supplierEntity = _supplierRepository.Get(x => x.Id == id);
        return supplierEntity;
    }

    public IEnumerable<SupplierEntity> GetAllSuppliers()
    {
        var suppliers = _supplierRepository.GetAll();
        return suppliers;
    }

    public SupplierEntity UpdateSupplier(SupplierEntity supplierEntity)
    {
        var updatedSupplier = _supplierRepository.Update(x => x.Id == supplierEntity.Id, supplierEntity);
        return updatedSupplier;
    }


    public void DeleteSupplier(int Id)
    {
        _supplierRepository.Delete(x => x.Id == Id);

    }
}
