using DbAssignment.Contexts;
using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DbAssignment.Repositories;

public class OrderItemRepository : BaseRepo<OrderItemEntity>
{

    private readonly DataContext _context;  
    public OrderItemRepository(DataContext context) : base(context)
    {

        _context = context;
    }

    public override OrderItemEntity Get(Expression<Func<OrderItemEntity, bool>> expression)
    {
        var entity = _context.OrderItems.Include(i => i.Product).Include(i => i.Order).FirstOrDefault(expression);

        return entity!;
    }

    public override IEnumerable<OrderItemEntity> GetAll()
    {
        return _context.OrderItems.Include(i => i.Product).Include(i => i.Order).ToList();
    }
}

