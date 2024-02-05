using DbAssignment.Contexts;
using DbAssignment.Entities;



namespace DbAssignment.Repositories;

public class UserRepository : BaseRepo<UserEntity>
{

   

    public UserRepository(DataContext context) : base(context)
    {

        

    }

   
}

