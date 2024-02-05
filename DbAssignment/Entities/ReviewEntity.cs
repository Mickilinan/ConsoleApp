using System.ComponentModel.DataAnnotations.Schema;

namespace DbAssignment.Entities;

public partial class ReviewEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; } = null!;

    [ForeignKey("UserId")]
    public UserEntity User { get; set; } = null!;

    public string Comment { get; set; } = null!;
}
