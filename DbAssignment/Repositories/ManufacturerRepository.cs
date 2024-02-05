using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class ManufacturerRepository : BaseRepo<DataContext2, ManufacturerEntity>
{
    public ManufacturerRepository(DataContext2 context) : base(context)
    {
    }
}

