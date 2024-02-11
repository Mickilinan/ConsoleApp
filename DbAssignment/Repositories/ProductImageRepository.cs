using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class ProductImageRepository(DataContext2 context) : BaseRepo<DataContext2, ProductImageEntity>(context)
{
}
