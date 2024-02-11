using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class ManufacturerRepository(DataContext2 context) : BaseRepo<DataContext2, ManufacturerEntity>(context)
{
}

