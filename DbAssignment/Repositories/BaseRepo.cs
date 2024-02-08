﻿using DbAssignment.Contexts;
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
        // Check if entity is null
        if (entity == null)
        {
            // If entity is null, return null
            return null;
        }

        // If entity is not null, update the entity
        var existingEntity = _context.Set<TEntity>().SingleOrDefault(expression);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
        return existingEntity;
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
