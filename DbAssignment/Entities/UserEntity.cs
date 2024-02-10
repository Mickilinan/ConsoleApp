using System.ComponentModel.DataAnnotations;

namespace DbAssignment.Entities;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public ICollection<OrderEntity> Orders { get; set; }
}
