using DbAssignment.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;

public class ProductAttributeService_Tests
{

    private readonly DataContext2 _context =
     new(new DbContextOptionsBuilder<DataContext2>()
         .UseInMemoryDatabase($"{Guid.NewGuid()}")
         .Options);
}
