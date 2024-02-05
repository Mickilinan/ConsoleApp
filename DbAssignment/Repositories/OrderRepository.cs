using DbAssignment.Contexts;
using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DbAssignment.Repositories;

public class OrderRepository : BaseRepo<DataContext,OrderEntity>
{

    private readonly DataContext _context;  
    public OrderRepository(DataContext context) : base(context)
    {

        _context = context;
    }

    public override OrderEntity Get(Expression<Func<OrderEntity, bool>> expression)
    {
        var entity = _context.Orders.Include(i => i.User).FirstOrDefault(expression);

        return entity!;
    }

    public override IEnumerable<OrderEntity> GetAll()
    {
        return _context.Orders.Include(i => i.User).ToList();
    }
}

