using DbAssignment.Contexts;
using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DbAssignment.Repositories;

public class OrderItemRepository(DataContext context) : BaseRepo<DataContext,OrderItemEntity>(context)
{

    private readonly DataContext _context = context;

    public override OrderItemEntity Get(Expression<Func<OrderItemEntity, bool>> expression)
    {
        var entity = _context.OrderItems
            .Include(x => x.Product)
            .Include(x => x.Order)
            .FirstOrDefault(expression);

        return entity!;
    }

    public override IEnumerable<OrderItemEntity> GetAll()
    {
        return _context.OrderItems.Include(x => x.Product).Include(x => x.Order).ToList();
    }
}

