using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class ProductAttributeRepository : BaseRepo<DataContext2, ProductAttributeEntity>
{
    public ProductAttributeRepository(DataContext2 context) : base(context)
    {
    }
}   

