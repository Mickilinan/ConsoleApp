using System.ComponentModel.DataAnnotations;

namespace DbAssignment.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; } 
    public string? Description { get; set; }


    public int CategoryId { get; set; } 
    public CategoryEntity Category { get; set; } = null!;

    public ICollection<OrderItemEntity> OrderItems { get; set; }

}
