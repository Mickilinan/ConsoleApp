using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class ProductImageRepository : BaseRepo<DataContext2, ProductImageEntity>
{
    public ProductImageRepository(DataContext2 context) : base(context)
    {
    }
}
