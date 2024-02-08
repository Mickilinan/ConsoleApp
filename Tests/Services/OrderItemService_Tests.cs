using DbAssignment.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services;
public class OrderItemService_Tests
{
    private readonly DataContext _context =
       new(new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);
}
