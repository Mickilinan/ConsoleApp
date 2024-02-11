using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class SupplierRepository(DataContext2 context) : BaseRepo<DataContext2, SupplierEntity>(context)
{
}
