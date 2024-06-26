﻿using DbAssignment.Contexts;
using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DbAssignment.Repositories;

public class OrderRepository(DataContext context) : BaseRepo<DataContext,OrderEntity>(context)
{

    private readonly DataContext _context = context;

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

