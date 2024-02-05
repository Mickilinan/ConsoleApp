using DbAssignment.Contexts;
using DbAssignment.Entities;



namespace DbAssignment.Repositories;

public class UserRepository : BaseRepo<DataContext,UserEntity>
{

   

    public UserRepository(DataContext context) : base(context)
    {

        

    }

   
}

