using DbAssignment.Contexts;
using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DbAssignment.Repositories;

public class ProductRepository(DataContext context) : BaseRepo<DataContext,ProductEntity>(context)
{

    private readonly DataContext _context = context;

    public override ProductEntity Get(Expression<Func<ProductEntity, bool>> expression)
    {
        var entity = _context.Products.Include(i => i.Category).FirstOrDefault(expression);

        return entity!;
    }

    public override IEnumerable<ProductEntity> GetAll()
    {
        return _context.Products.Include(i => i.Category).ToList();
    }
}

