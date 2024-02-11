using DbAssignment.Contexts;
using DbAssignment.Entities;
using System.Linq.Expressions;



namespace DbAssignment.Repositories;

public class UserRepository(DataContext context) : BaseRepo<DataContext, UserEntity>(context)
{

    private readonly DataContext _context = context;

    public override UserEntity Get(Expression<Func<UserEntity, bool>> expression)
    {

        return _context.Users.FirstOrDefault(expression);
    }

}

    


