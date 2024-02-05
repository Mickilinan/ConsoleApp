using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class SupplierRepository : BaseRepo<DataContext2, SupplierEntity>
{
    public SupplierRepository(DataContext2 context) : base(context)
    {
    }
}
