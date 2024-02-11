using DbAssignment.Contexts;
using DbAssignment.Entities;


namespace DbAssignment.Repositories;

public class ReviewRepository(DataContext2 context) : BaseRepo<DataContext2, ReviewEntity>(context)
{
}
