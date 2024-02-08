using DbAssignment.Contexts;
using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



namespace DbAssignment.Repositories;

public class UserRepository : BaseRepo<DataContext, UserEntity>
{

    private readonly DataContext _context;

    public UserRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override UserEntity Get(Expression<Func<UserEntity, bool>> expression)
    {

        return _context.Users.FirstOrDefault(expression);
    }

}

    


