using System.ComponentModel.DataAnnotations;

namespace DbAssignment.Entities;

public class OrderEntity
{
    [Key]   
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public string Status { get; set; } = null!; 

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


}
