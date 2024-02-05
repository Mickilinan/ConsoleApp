

using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class CategoryRepository : BaseRepo<CategoryEntity>
{
    public CategoryRepository(DataContext context) : base(context)
    {
    }


}

