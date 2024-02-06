using DbAssignment.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DbAssignment.Repositories;

public class BaseRepo<TContext, TEntity> where TContext : DbContext where TEntity : class
{


    private readonly TContext _context;

    public BaseRepo(TContext context)
    {
        _context = context;
    }


    public virtual TEntity Create(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public virtual TEntity Get(Expression<Func<TEntity, bool>> expression)
    {
        var entity = _context.Set<TEntity>().FirstOrDefault(expression);
        return entity!;
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public virtual IEnumerable <TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Where(expression).ToList();
       
    }


    public virtual TEntity Update(Expression<Func<TEntity, bool>> expression, TEntity entity)
    {
        var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(expression);
        _context.Entry(entityToUpdate!).CurrentValues.SetValues(entity);
        _context.SaveChanges();
        return entityToUpdate!;
    }

    public virtual bool Delete(Expression<Func<TEntity, bool>> expression)
    {
        var entity = _context.Set<TEntity>().FirstOrDefault(expression);
        if (entity == null)
        {
            return false;
        }
        _context.Remove(entity!);
        _context.SaveChanges();

        return true;
        
        
    }
}
