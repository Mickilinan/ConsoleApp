using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class CategoryRepository(DataContext context) : BaseRepo<DataContext,CategoryEntity>(context)
{
}

