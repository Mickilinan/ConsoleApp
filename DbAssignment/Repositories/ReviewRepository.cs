using DbAssignment.Contexts;
using DbAssignment.Entities;


namespace DbAssignment.Repositories;

public class ReviewRepository : BaseRepo<DataContext2, ReviewEntity>
{
    
    public ReviewRepository(DataContext2 context) : base(context)
    {

        
    }

}
