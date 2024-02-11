using DbAssignment.Contexts;
using DbAssignment.Entities;

namespace DbAssignment.Repositories;

public class ProductAttributeRepository(DataContext2 context) : BaseRepo<DataContext2, ProductAttributeEntity>(context)
{
}   

